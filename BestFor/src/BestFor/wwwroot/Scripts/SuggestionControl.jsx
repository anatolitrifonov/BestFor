// If you need to interact with the browser, perform your work in componentDidMount() or the other lifecycle methods instead.

/*
Control to show two text box fields search form
State: suggestionsData - last list of suggestions loaded from the server
*/
var SuggestionControl = React.createClass({
    // built in ability to verify type of each property
    propTypes: {
        // not required but very useful
        // onUserTyping event handler should be a function
        suggestionsUrl: React.PropTypes.string
    },

    // built in ablity to set initial state
    getInitialState: function () {
        // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
        // initial set of suggestions is blank
        return {
            suggestionsData: [],
            listTop: 110,
            listLeft: 20,
            showList: false,
            currentValue: ""
        };
    },

    // Convert request readyState from number to text
    readyStateToText: function (readyState) {
        if (readyState == 0) return "UNSENT open() has not been called yet.";
        if (readyState == 1) return "OPENED send() has been called.";
        if (readyState == 2) return "HEADERS_RECEIVED send() has been called, and headers and status are available.";
        if (readyState == 3) return "LOADING Downloading; responseText holds partial data.";
        if (readyState == 1) return "DONE The operation is complete.";
        return "Unknown";
    },

    // handle user typing text in the word box load data and show in usggestions popup
    handleUserTyping: function (userInputObject) {
        // update the state no matter what. even if we are not launching the server request
        this.setState({ currentValue: userInputObject.Phrase });

        // console.log("user typing was handled");
        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            console.log("xhr is busy with state " + this.xhr.readyState + " " + this.readyStateToText(this.xhr.readyState));
            return;
        }

        if (userInputObject == null || userInputObject.Phrase == null || userInputObject.Phrase.length < 3) return;

        // build url passing user input
        var url = this.props.suggestionsUrl + "?userInput=" + userInputObject.Phrase;
        this.xhr = new XMLHttpRequest();
        this.xhr.open("get", url, true);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            console.log("xhr onload returned " + this.xhr.responseText);
            var httpResultData = JSON.parse(this.xhr.responseText); // could also go through event
            // this is expected to update the list that is bound to this state data.
            this.setState({
                suggestionsData: httpResultData,
                listTop: userInputObject.y,
                listLeft: userInputObject.x,
                showList: true
            });
            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this); 
        this.xhr.onerror = function () {
            console.log("xhr onerror returned " + this.xhr.responseText);
        };
        this.xhr.send();
    },

    // handle user clicking on the suggestions list. hide it and set the text
    handleListClicked: function (phrase) {
        this.setState({
            showList: false, // hide list
            currentValue: phrase.Phrase // set the value in the text box
        });
    },

    render: function () {
        return (
            <span>
                <SuggestionTextBox onUserTyping={this.handleUserTyping} textValue={this.state.currentValue} />
                <SuggestionResultList suggestions={this.state.suggestionsData}
                                  listTop={this.state.listTop}
                                  listLeft={this.state.listLeft}
                                  onListClicked={this.handleListClicked}
                                  isVisible={this.state.showList}/>
            </span>
        );
    }
});
