var SuggestionPanel = React.createClass({
    // Built in ability to verify type of each property
    propTypes: {
        // not required but very useful
        // URL to load suggestions 
        suggestionsUrl: React.PropTypes.string,
        // URL to load answers
        answersUrl: React.PropTypes.string,
        // token given by the controller to send back as verification
        antiForgeryToken: React.PropTypes.string,
        // token is expected to be sent in this header
        antiForgeryHeaderName: React.PropTypes.string,
        // resource strings are given by the user in this design.
        resourceStrings: React.PropTypes.object,
        // culture for building links
        culture: React.PropTypes.string,
        // produce debugging info?
        debug: React.PropTypes.bool
    },

    // List of strings this control wants in resourceStrings:
    // this.props.resourceStrings.suggestion_panel_initial_message
    // suggestion_panel_initial_message
    // suggestion_panel_no_answers_found : "No answers found. Be the first!"
    // suggestion_panel_x_answers_found : " answers found. Do you have your own? Or vote for the answer below?"
    // suggestion_panel_error_happened_searching_answers : "Error happened while searching for answers"
    // suggestion_panel_you_were_the_first : "You were the first to say that
    // best_start_capital : "Best"
    // for_lower : "for"
    // is_lower : "is"
    // suggestion_panel_your_answer : "Your answer"
    // suggestion_panel_was_added : "was added"
    // suggestion_panel_this_answer_was_given : "This answer was given"
    // times_lower : "times"
    // suggestion_panel_extended_opinion : "Would you like to add an extended opinion?"
    // add_capital : "Add"
    // search_capital : "Search"
    // last_100: "Everything"


    // Built in ability to set initial state
    getInitialState: function () {
        // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
        // initial set of suggestions is blank
        return {
            statusMessage: this.props.resourceStrings.suggestion_panel_initial_message,  // used for guiding users on what to do
            answers: [],                            // data loaded from servers
            showErrorPane: false,                   // helps displaying error panel
            errorMessage: "",                       // this.props.antiForgeryToken // error message to show
            lastAddedAnswerId: 0,                   // id of the last added answer
            showAddDescriptionLink: false           // do not show add description link on load
        };
    },

    // Static functions that are ... well just static functions ... they can not access state
    statics: {
        // Validate user input 
        //todo. Need to check for special characters?
        validateInput: function (inputValue) {
            if (inputValue == null) return false;
            if (inputValue.trim().length < 3) return false;
            return true;
        },

        writeDebug(debug, message) {
            if (debug) console.log("SuggestionPanel: " + message);
        },

        createAddAnswerData: function (leftValue, rightValue, answer, debug) {
            var leftValueValidationResult = SuggestionPanel.validateInput(leftValue);
            if (leftValueValidationResult) {
                SuggestionPanel.writeDebug(debug, "Left value " + leftValue + " a is good");
            }
            else {
                SuggestionPanel.writeDebug(debug, "Left value " + leftValue + " is bad. Returning.");
                return null;
            }
            var rightValueValidationResult = SuggestionPanel.validateInput(rightValue);
            if (rightValueValidationResult) {
                SuggestionPanel.writeDebug(debug, "Right value " + rightValue + "a is good");
            }
            else {
                SuggestionPanel.writeDebug(debug, "Right value " + rightValue + " is bad. Returning.");
                return null;
            }
            var answerValueValidationResult = SuggestionPanel.validateInput(answer);
            if (answerValueValidationResult) {
                SuggestionPanel.writeDebug(debug, "Answer value " + answer + "a is good");
            }
            else {
                SuggestionPanel.writeDebug(debug, "Answer value " + answer + " is bad. Returning.");
                return null;
            }
            var data = new FormData();
            data.append("LeftWord", leftValue);
            data.append("RightWord", rightValue);
            data.append("Phrase", answer);
            return data;
        },

        createSearchAnswersData: function (leftValue, rightValue, debug) {
            var leftValueValidationResult = SuggestionPanel.validateInput(leftValue);
            if (leftValueValidationResult) {
                SuggestionPanel.writeDebug(debug, "createSearchAnswersData left value " + leftValue + " is good");
            }
            else {
                SuggestionPanel.writeDebug(debug, "createSearchAnswersData left value " + leftValue + " is bad. Returning null.");
                return null;
            }
            var rightValueValidationResult = SuggestionPanel.validateInput(rightValue);
            if (rightValueValidationResult) {
                SuggestionPanel.writeDebug(debug, "createSearchAnswersData right value " + rightValue + " is good");
            }
            else {
                SuggestionPanel.writeDebug(debug, "createSearchAnswersData right value " + rightValue + " is bad. Returning null.");
                return null;
            }
            
            return "leftWord=" + leftValue + "&rightWord=" + rightValue;
        },

        // Check if we anready have this answer in the list
        updateAnswersLocally: function (leftValue, rightValue, phrase, answers) {
            // assume that current answers are for this left word and this right word and simply loop the array
            if (answers == null) return;
            if (answers.length == null) return;
            for (var i = 0; i < answers.length; i++)
                if (answers[i].phrase == phrase) {
                    // found it in the list. Up the count. This actually does reflect on UI.
                    answers[i].Count++;
                    return;
                }
            // add to the end
            answers.push({ LeftWord: leftValue, RightWord: rightValue, Phrase: phrase, Count: 1 });
        },
    },

    // Navigate to a search page
    doNavigationSearchFromButton: function() {
        var leftTextBoxValue = this.leftTextBox.getCurrentValue();
        var leftTextBoxValidationResult = SuggestionPanel.validateInput(leftTextBoxValue);
        var rightTextBoxValue = this.rightTextBox.getCurrentValue();
        var rightTextBoxValidationResult = SuggestionPanel.validateInput(rightTextBoxValue);
        var data = "";
        var url = "/";
        // if only left is filled in search left
        if (!leftTextBoxValidationResult && !rightTextBoxValidationResult) {
            SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel not going to search. Empty data.");
            return;
        }
        else if (leftTextBoxValidationResult && !rightTextBoxValidationResult) {
            url += "left/";
            data = leftTextBoxValue.trim();
        }
        else {
            url += "right/";
            data = rightTextBoxValue.trim();
        }
        
        // escape ???
        url += data;
        window.location.href = url;
    },

    // Handles answers search launch from search button
    // This is old and no used right now
    doAnswersSearchFromButton: function() {
        this.doAnswersSearch(this.leftTextBox.getCurrentValue(), this.rightTextBox.getCurrentValue());
    },

    // Handles answers search launch from the left box
    doAnswersSearchFromLeftTextBox: function (phrase) {
        this.doAnswersSearch(phrase, this.rightTextBox.getCurrentValue());
    },

    // Handles answers search launch from the right box
    doAnswersSearchFromRightTextBox: function (phrase) {
        SuggestionPanel.writeDebug(this.props.debug, "doAnswersSearchFromRightTextBox");
        this.doAnswersSearch(this.leftTextBox.getCurrentValue(), phrase);
    },

    // Launch the search for existing answers
    doAnswersSearch: function(leftWord, rightWord) {
        SuggestionPanel.writeDebug(this.props.debug, "click the search button was handled leftWord:" + leftWord + " rightWord:" + rightWord);

        // No point in doing anything if answer URL was not given
        if (this.props.answersUrl === null || this.props.answersUrl === "") return null;

        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel xhr is busy with state " + this.xhr.readyState +
                " " + SuggestionControl.readyStateToText(this.xhr.readyState));
            return;
        }

        // build url passing user input
        var userInput = SuggestionPanel.createSearchAnswersData(leftWord, rightWord, this.props.debug);

        // get out if data is invalid
        if (userInput == null) return;

        var url = this.props.answersUrl + "?" + userInput;
        if (this.xhr == null) this.xhr = new XMLHttpRequest();
        this.xhr.open("get", url, true);
        // add header for antiforgery validation if header was set as a property
        if (this.props.antiForgeryHeaderName != null || this.props.antiForgeryHeaderName != "")
            this.xhr.setRequestHeader(this.props.antiForgeryHeaderName, this.props.antiForgeryToken);
        // handle received data.
        // onreadystatechange
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            // if all good
            if (this.xhr.status === 200) {
                SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel xhr onload returned " + this.xhr.responseText);
                var httpResultData = JSON.parse(this.xhr.responseText); // could also go through event
                if (httpResultData == null || httpResultData.errorMessage != null) {
                    this.processErrorInAnswersSearch(httpResultData.errorMessage);
                }
                else {
                    this.processFoundAnswers(httpResultData.answers);
                }
            }
            // all is bad
            else
            {
                this.processErrorInAnswersSearch(this.xhr.responseText);
            }
            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this);
        // this.xhr.onerror -> Chrome does not implement onerror
        this.xhr.send();
    },

    // Handles successful search for opinions
    processFoundAnswers: function (answers) {
        var message = this.props.resourceStrings.suggestion_panel_no_answers_found;
        if (answers != null && answers.length > 0)
            message = answers.length + " " + this.props.resourceStrings.suggestion_panel_x_answers_found;
        // Save the answers to use later
        this.answers = answers;
        // this is expected to update the list that is bound to this state data.
        this.setState({
            // statusMessage: message, // message
            answers: this.answers, // set aswers data
            showErrorPane: false, // hide errors
            errorMessage: "", // blank the error 
            showAddDescriptionLink: false,
            answerResultMessage: message
        });
    },

    // Handles ubsuccessful search for opinions
    processErrorInAnswersSearch: function (errorMessage) {
        SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel xhr onload errored out. Error text is probably too long.");
        this.answers = [];
        this.setState({
            statusMessage: this.props.resourceStrings.suggestion_panel_error_happened_searching_answers, // message
            answers: this.answers, // blank out the aswers
            showErrorPane: true, // show errors
            errorMessage: errorMessage, // show error details
            showAddDescriptionLink: false
        });
    },

    // Handle user clicking on the list of answers to fill in the right textbox
    handleOnListClicked: function (phrase) {
        ReactDOM.findDOMNode(this.answerTextBox).value = phrase.Phrase;
    },

    // Add answer to the server
    handleAddButtonClick: function () {
        //  First check if the url for the answers was set
        if (this.props.answersUrl == null || this.props.answersUrl == "") {
            SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel handleAddButtonClick answersUrl property is not set. Can't send data.");
        }

        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel handleAddButtonClick xhr is busy with state " + this.xhr.readyState +
                " " + SuggestionControl.readyStateToText(this.xhr.readyState));
            return;
        }
        // create post data object from the content of this control
        var postData = SuggestionPanel.createAddAnswerData(
            this.leftTextBox.getCurrentValue(), 
            this.rightTextBox.getCurrentValue(),
            ReactDOM.findDOMNode(this.answerTextBox).value, this.props.debug);

        // return if could not create form data
        if (postData == null) return;
        
        // Since the post if happening let's optimiztically update the list
        SuggestionPanel.updateAnswersLocally(this.leftTextBox.getCurrentValue(), this.rightTextBox.getCurrentValue(),
            ReactDOM.findDOMNode(this.answerTextBox).value, this.answers);

        // build url passing user input
        if (this.xhr == null) this.xhr = new XMLHttpRequest();
        this.xhr.open("post", this.props.answersUrl, true);
        // add header for antiforgery validation if header was set as a property
        if (this.props.antiForgeryHeaderName != null || this.props.antiForgeryHeaderName != "")
            this.xhr.setRequestHeader(this.props.antiForgeryHeaderName, this.props.antiForgeryToken);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            // if all good
            if (this.xhr.status === 200) {
                this.processAddedResult();
            }
            // all is bad
            else {
                SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel handleAddButtonClick xhr onload errored out. Error text is probably too long.");
                this.setState({
                    statusMessage: this.props.resourceStrings.suggestion_panel_error_happened_searching_answers, // message
                    showErrorPane: true, // show errors
                    errorMessage: this.xhr.responseText, // show error details
                    showAddDescriptionLink: false
                });
            }
            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this);
        this.xhr.send(postData);
    },

    // Handles successful answer addition
    processAddedResult: function() {
        SuggestionPanel.writeDebug(this.props.debug, "SuggestionPanel handleAddButtonClick xhr onload returned " + this.xhr.responseText);
        var httpResultData = JSON.parse(this.xhr.responseText);
        // Let's look at the data. May be there was an error
        var r = this.props.resourceStrings;
        // you were the first to say that "best blah for blah is blah"!
        var quotedAnswer = "\"" + r.best_start_capital + " " + httpResultData.answer.leftWord +
            " " + r.for_lower + " " + httpResultData.answer.rightWord +
            " " + r.is_lower + " " + httpResultData.answer.phrase + "\"";
        var message = r.suggestion_panel_you_were_the_first + quotedAnswer + "!";
        var showErrorPane = false;
        if (httpResultData.errorMessage != null) {
            showErrorPane = true;
            message = httpResultData.errorMessage;
        }
        else if (httpResultData.answer.count > 1) {
            // your answer "best blah for blah is blah" was added. This answer was given Z times.
            message = r.suggestion_panel_your_answer + " " + quotedAnswer + " " + r.suggestion_panel_was_added + ". " +
                r.suggestion_panel_this_answer_was_given + " " + httpResultData.answer.count + " " + r.times_lower + ".";
        }
 
        // clear the form
        this.clearTheForm();
        this.setState({
            statusMessage: message,         // message
            showErrorPane: showErrorPane,   // hide errors
            errorMessage: message,          // blank the error,
            answers: this.answers,          // set aswers data
            lastAddedAnswerId: httpResultData.answer.id, // set the id of the last added answer
            showAddDescriptionLink: true    // do offer user to add extended opinion
        });
    },

    // Clear the form
    clearTheForm: function () {
        ReactDOM.findDOMNode(this.answerTextBox).value = "";
        this.leftTextBox.blankCurrentValue();
        this.rightTextBox.blankCurrentValue();
        this.answers = [];
        this.setState({
            answerResultMessage: "",
            showAddDescriptionLink: false
        });
    },

    // Fires when left box gets the focus
    handleLeftTextBoxGotFocus: function () {
        this.handleTextBoxGotFocus(1);
    },

    // Fires when right box gets the focus
    handleRightTextBoxGotFocus: function () {
        this.handleTextBoxGotFocus(2);
    },

    // Fires when answer box gets the focus
    handleAnswerTextBoxGotFocus: function () {
        this.handleTextBoxGotFocus(3);
    },

    // change the status message depending on what's going on
    handleTextBoxGotFocus: function (whichOne) {
        if (this.leftTextBox == null || this.rightTextBox == null || this.answerTextBox == null) return;
        var leftGotFocus = whichOne == 1;
        var rightGotFocus = whichOne == 2;
        var answerGotFocus = whichOne == 3;
        // Get validity
        var leftValid = SuggestionPanel.validateInput(this.leftTextBox.getCurrentValue());
        var rightValid = SuggestionPanel.validateInput(this.rightTextBox.getCurrentValue());
        var answerValid = SuggestionPanel.validateInput(ReactDOM.findDOMNode(this.answerTextBox).value);
        // Only set message in specific situations.
        if (leftValid && !answerValid && !answerGotFocus) {
            this.setState({
                statusMessage: "Enter the second word",
                showAddDescriptionLink: false
            });
        }
        else if (leftValid && rightValid && !answerValid && answerGotFocus) {
            this.setState({
                statusMessage: "Enter the answer",
                showAddDescriptionLink: false
            });
        }
            // Any valid make sure button is hidden
        else if (leftValid || rightValid || answerValid) {
            this.setState({
                showAddDescriptionLink: false
            });
        }
    },

    render: function () {
        var errorDisplayStyle = {
            display: this.state.showErrorPane ? "" : "none",
            width: 500,
            height:500
        };

        var leftImageStyle = {
            position: "absolute",
            bottom: "0px",
            left: "100px"
        };
        var addDescriptionStyle = {
            display: this.state.showAddDescriptionLink ? "" : "none"
        };
        var linkToExtendedOpinion = "/" + this.props.culture + "/AnswerAction/AddDescription?answerId=" + this.state.lastAddedAnswerId;
        var linkToEverything = "/" + this.props.culture + "/Search/Everything";

        return (
            <div>
                <img src="/images/woman.png" width="174" height="245" alt="" style={leftImageStyle} />

                <div className="col-sm-8 col-sm-offset-4">
                    <span className="best-light-text">{ this.state.statusMessage }</span>
                    <div style={ addDescriptionStyle }>
                        <a className="btn best-index-button"
                           href={linkToExtendedOpinion}>{this.props.resourceStrings.suggestion_panel_extended_opinion}</a>
                    </div>

                    {/* This will be known as leftTextBox */}
                    <SuggestionControl suggestionsUrl={this.props.suggestionsUrl} onValueChange={this.doAnswersSearchFromLeftTextBox}
                                       antiForgeryToken={this.props.antiForgeryToken} antiForgeryHeaderName={this.props.antiForgeryHeaderName}
                                       ref={(ref) => this.leftTextBox = ref} focusOnLoad={ true } textBoxGroupId={ "first-text-box" }
                                       labelText={this.props.resourceStrings.best_start_capital} onGotFocus={this.handleLeftTextBoxGotFocus}
                                       debug={this.props.debug}
                                       placeHolder="start typing" />
                    {/* This will be knows as rightTextBox */}
                    <SuggestionControl suggestionsUrl={this.props.suggestionsUrl} onValueChange={this.doAnswersSearchFromRightTextBox}
                                       antiForgeryToken={this.props.antiForgeryToken} antiForgeryHeaderName={this.props.antiForgeryHeaderName}
                                       ref={(ref) => this.rightTextBox = ref} focusOnLoad={ false } textBoxGroupId={ "second-text-box" }
                                       labelText={this.props.resourceStrings.for_lower} onGotFocus={this.handleRightTextBoxGotFocus}
                                       debug={this.props.debug}
                                       placeHolder="continue here"/>
                    {/* This will be knows as answerTextBox */}
                    <div className="input-group best-some-padding">
                        <span className="input-group-addon index-page-label" id="basic-addon3">{this.props.resourceStrings.is_lower}:</span>
                        <input type="text" className="form-control index-page-input" aria-describedby="basic-addon3"
                               placeholder="finish with your opinion"
                               ref={(ref) => this.answerTextBox = ref}
                               onChange={this.doAnswersSearchFromButton}
                               onFocus={this.handleAnswerTextBoxGotFocus} />
                    </div>
                    <div className="best-some-padding">
                        <input type="button" value={this.props.resourceStrings.add_capital}
                               onClick={this.handleAddButtonClick} className="btn best-index-button" />
                        <input type="button" value={this.props.resourceStrings.search_capital}
                               onClick={this.doNavigationSearchFromButton} className="btn best-index-button" />
                        {/* a href={linkToEverything} 
                              className="btn best-index-button best-some-padding"{this.props.resourceStrings.last_100} */}
                    </div>
                    <span className="best-light-text">{ this.state.answerResultMessage }</span>
                    <SuggestionAnswerList answers={this.state.answers} onListClicked={this.handleOnListClicked} culture={this.props.culture} />
                    <textarea style={errorDisplayStyle} ref={(ref) => this.errorDisplay = ref} value={this.state.errorMessage} readOnly />
                </div>
            </div>
        );
    }
});
