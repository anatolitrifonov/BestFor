var SuggestionPanel = React.createClass({
    // built in ability to verify type of each property
    propTypes: {
        // not required but very useful
        // URL to load suggestions 
        suggestionsUrl: React.PropTypes.string,
        // URL to load answers
        answersUrl: React.PropTypes.string
    },

    // built in ablity to set initial state
    getInitialState: function () {
        // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
        // initial set of suggestions is blank
        return {
            statusMessage: "Enter the first word",
            answers: []
        };
    },

    // Static functions that are ... welll just static functions ... they can not access state
    statics: {
        // Validate user input 
        //todo. Need to check for special characters?
        validateInput: function (inputValue) {
            if (inputValue == null) return false;
            if (inputValue.length < 3) return false;
            return true;
        },

        createAddAnswerData: function (leftValue, rightValue, answer) {
            var leftValueValidationResult = SuggestionPanel.validateInput(leftValue);
            if (leftValueValidationResult)
                console.log("Left value " + leftValue + " a is good");
            else {
                console.log("Left value " + leftValue + " is bad. Returning.");
                return null;
            }
            var rightValueValidationResult = SuggestionPanel.validateInput(rightValue);
            if (rightValueValidationResult)
                console.log("Right value " + rightValue + "a is good");
            else {
                console.log("Right value " + rightValue + " is bad. Returning.");
                return null;
            }
            var answerValueValidationResult = SuggestionPanel.validateInput(answer);
            if (answerValueValidationResult)
                console.log("Answer value " + answer + "a is good");
            else {
                console.log("Answer value " + answer + " is bad. Returning.");
                return null;
            }
            var data = new FormData();
            data.append("LeftWord", leftValue);
            data.append("RightWord", rightValue);
            data.append("Phrase", answer);
            return data;
        },

        createSearchAnswersData: function (leftValue, rightValue) {
            var leftValueValidationResult = SuggestionPanel.validateInput(leftValue);
            if (leftValueValidationResult)
                console.log("Left value " + leftValue + " is good");
            else {
                console.log("Left value " + leftValue + " is bad. Returning.");
                return null;
            }
            var rightValueValidationResult = SuggestionPanel.validateInput(rightValue);
            if (rightValueValidationResult)
                console.log("Right value " + rightValue + " is good");
            else {
                console.log("Right value " + rightValue + " is bad. Returning.");
                return null;
            }
            
            return "leftWord=" + leftValue + "&rightWord=" + rightValue;
        }
    },

    // Handles answers search launch from buttons
    doAnswersSearchFromButton: function() {
        this.doAnswersSearch(this.leftTextBox.getCurrentValue(), this.rightTextBox.getCurrentValue());
    },

    // Handles answers search launch from the left box
    doAnswersSearchFromLeftTextBox: function (phrase) {
        this.doAnswersSearch(phrase, this.rightTextBox.getCurrentValue());
    },

    // Handles answers search launch from the right box
    doAnswersSearchFromRightTextBox: function (phrase) {
        this.doAnswersSearch(this.leftTextBox.getCurrentValue(), phrase);
    },

    // Launch the search for existing answers
    doAnswersSearch: function(leftWord, rightWord) {
        console.log("click the search button was handled leftWord:" + leftWord + " rightWord:" + rightWord);
        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            console.log("SuggestionPanel xhr is busy with state " + this.xhr.readyState +
                " " + SuggestionControl.readyStateToText(this.xhr.readyState));
            return;
        }

        // build url passing user input
        var userInput = SuggestionPanel.createSearchAnswersData(leftWord, rightWord);
        // get out if data is invalid
        if (userInput == null) return;

        var url = this.props.answersUrl + "?" + userInput;
        this.xhr = new XMLHttpRequest();
        this.xhr.open("get", url, true);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            console.log("SuggestionPanel xhr onload returned " + this.xhr.responseText);
            var httpResultData = JSON.parse(this.xhr.responseText); // could also go through event
            // this is expected to update the list that is bound to this state data.
            this.setState({
                answers: httpResultData
            });
            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this);
        this.xhr.onerror = function () {
            console.log("xhr onerror returned " + this.xhr.responseText);
        };
        this.xhr.send();
    },

    // Handle user clicking on the list of answers to fill in the right textbox
    handleOnListClicked: function (phrase) {
        this.answerTextBox.getDOMNode().value = phrase.Phrase;
    },

    // Add answer to the server
    handleAddButtonClick: function () {
        //  First check if the url for the answers was set
        if (this.props.answersUrl == null || this.props.answersUrl == "") {
            console.log("SuggestionPanel handleAddButtonClick answersUrl property is not set. Can't send data.");
        }

        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            console.log("SuggestionPanel handleAddButtonClick xhr is busy with state " + this.xhr.readyState +
                " " + SuggestionControl.readyStateToText(this.xhr.readyState));
            return;
        }
        // create post data object from the content of this control
        var postData = SuggestionPanel.createAddAnswerData(
            this.leftTextBox.getCurrentValue(), 
            this.rightTextBox.getCurrentValue(),
            this.answerTextBox.getDOMNode().value);

        // return if could not create form data
        if (postData == null) return;
        
        // build url passing user input
        this.xhr = new XMLHttpRequest();
        this.xhr.open("post", this.props.answersUrl, true);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            console.log("SuggestionPanel handleAddButtonClick xhr onload returned " + this.xhr.responseText);
            //var httpResultData = JSON.parse(this.xhr.responseText); // could also go through event
            // this is expected to update the list that is bound to this state data.
            //this.setState({
            //    answers: httpResultData
            //});
            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this);
        this.xhr.onerror = function () {
            console.log("SuggestionPanel handleAddButtonClick xhr onerror returned " + this.xhr.responseText);
        };
        this.xhr.send(postData);
    },

    render: function () {
        return (
            <div>
                <span>{ this.state.statusMessage }</span><br />
                Best<br />
                {/* This will be knows as leftTextBox */}
                <SuggestionControl suggestionsUrl={this.props.suggestionsUrl} onValueChange={this.doAnswersSearchFromLeftTextBox}
                                   ref={(ref) => this.leftTextBox = ref} /><br />
                for<br />
                {/* This will be knows as rightTextBox */}
                <SuggestionControl suggestionsUrl={this.props.suggestionsUrl} onValueChange={this.doAnswersSearchFromRightTextBox}
                                   ref={(ref) => this.rightTextBox = ref} /><br />
                is<br />
                {/* This will be knows as answerTextBox */}
                <input type="text" placeholder="your answer" ref={(ref) => this.answerTextBox = ref} onChange={this.doAnswersSearchFromButton}
                       className="AnswerTextBox"/>
                <input type="button" value="Add" onClick={this.handleAddButtonClick} />
                <input type="button" value="Search" onClick={this.doAnswersSearchFromButton} /><br /><br />
                <SuggestionAnswerList answers={this.state.answers} onListClicked={this.handleOnListClicked}/>
            </div>
        );
    }
});
