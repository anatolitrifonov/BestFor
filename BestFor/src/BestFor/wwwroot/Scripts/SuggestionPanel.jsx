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

    // Mouse click -> bubble up and say that we are not selected
    handleSearchButtonClick: function () {
        // update the state no matter what. even if we are not launching the server request
        //this.setState({ currentValue: userInputObject.Phrase });

        console.log("click the search button was handled");
        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            console.log("SuggestionPanel xhr is busy with state " + this.xhr.readyState + " " + this.readyStateToText(this.xhr.readyState));
            return;
        }

        // if (userInputObject == null || userInputObject.Phrase == null || userInputObject.Phrase.length < 3) return;

        // build url passing user input
        var url = this.props.answersUrl + "?leftWord=absd&rightWord=sdfsdf"; // + userInputObject.Phrase;
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

    render: function () {
        return (
            <div>
                <span>{ this.state.statusMessage }</span><br />
                <SuggestionControl suggestionsUrl={ this.props.suggestionsUrl } />&nbsp;&nbsp;
                <SuggestionControl suggestionsUrl={ this.props.suggestionsUrl } />&nbsp;&nbsp;
                <input type="text" placeholder="your answer" />
                <input type="button" value="Search" onClick={ this.handleSearchButtonClick } /><br /><br />
                <SuggestionAnswerList answers={ this.state.answers } />
            </div>
        );
    }
});
