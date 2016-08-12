// One SuggestionTextBox and one SuggestionResultList
// If you need to interact with the browser, perform your work in componentDidMount() or the other lifecycle methods instead.
/*
Control to show two text box fields search form
State: suggestionsData - last list of suggestions loaded from the server
*/
var SuggestionControl = React.createClass({
    // Built in ability to verify type of each property
    propTypes: {
        // not required but very useful
        suggestionsUrl: React.PropTypes.string,        // suggestionsUrl event handler should be a string
        onValueChange: React.PropTypes.func,           // onValueChange event handler should be a function
        onGotFocus: React.PropTypes.func,              // onGotFocus event handler should be a function
        focusOnLoad: React.PropTypes.bool,             // focusOnLoad is aboolean
        antiForgeryToken: React.PropTypes.string,      // token given by the controller to send back as verification
        antiForgeryHeaderName: React.PropTypes.string, // token is expected to be sent in this header
        labelText: React.PropTypes.string,             // Give this value to label
        inputGroupId: React.PropTypes.string,          // This id will go into input group
        placeHolder: React.PropTypes.string,           // Placeholder for the text box
        debug: React.PropTypes.bool                    // produce debugging info?
},

    // Built in ability to set initial state
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

    // Built in notification allowing to know that we need to clean thigs up
    componentWillUnmount: function () {
        // clearInterval is javascript built in. It stopps the ticking.
        if (this.interval != null) clearInterval(this.interval);
    },

    // Static functions that are ... welll just static functions ... they can not access state
    statics: {
        // Convert request readyState from number to text        
        readyStateToText: function (readyState) {
            if (readyState == 0) return "UNSENT open() has not been called yet.";
            if (readyState == 1) return "OPENED send() has been called.";
            if (readyState == 2) return "HEADERS_RECEIVED send() has been called, and headers and status are available.";
            if (readyState == 3) return "LOADING Downloading; responseText holds partial data.";
            if (readyState == 1) return "DONE The operation is complete.";
            return "Unknown";
        },

        writeDebug(debug, message) {
            if (debug) console.log("SuggestionControl: " + message);
        },
    },

    // list of suggestions is shown -> start ticking and checking if we need to autohide the list
    startTicking: function () {
        // if we are already ticking let it tick
        // clearInterval is javascript built in. It stopps the ticking.
        if (this.interval != null) clearInterval(this.interval);
        // setInterval is javascript built in. It fires function every interval until stopped.
        // This is the timer in milliseconds that checks if it is time to close the list.
        this.interval = setInterval(this.tick, 2000);
        SuggestionControl.writeDebug(this.props.debug, "started ticking");
    },

    // Check if list is inactive and close it
    tick: function () {
        SuggestionControl.writeDebug(this.props.debug, "ticked this.listIsActive = " + this.listIsActive + " interval = " + this.interval);
        if (this.interval != null && !this.listIsActive) {
            // clearInterval is javascript built in. It stopps the ticking.
            clearInterval(this.interval);
            // hide the list
            this.setState({ showList: false });
        }
    },

    //Get the current value
    getCurrentValue: function () {
        return this.state.currentValue;
    },

    // Blank out the value
    blankCurrentValue: function () {
        this.listIsActive = false;
        this.setState({
            showList: false, // hide list
            currentValue: ""
        });
    },

    // Handle user typing text in the word box load data and show in usggestions popup
    handleUserTyping: function (userInputObject) {
        // update the state no matter what. even if we are not launching the server request
        this.setState({ currentValue: userInputObject.Phrase });

        // Let the listeners know that something has changed
        this.notifyListenersOnValueChange(userInputObject.Phrase);

        // We are going to handle only one request at a time.
        // check if there is a request is process already
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            SuggestionControl.writeDebug(this.props.debug, "xhr is busy with state " + this.xhr.readyState +
                " " + SuggestionControl.readyStateToText(this.xhr.readyState));
            return;
        }

        // Do not remove .trim because searching for "tv " is no good.
        if (userInputObject == null || userInputObject.Phrase == null || userInputObject.Phrase.trim().length < 3) return;

        // build url passing user input
        var url = this.props.suggestionsUrl + "?userInput=" + encodeURIComponent(userInputObject.Phrase);
        if (this.xhr == null) this.xhr = new XMLHttpRequest();
        this.xhr.open("get", url, true);
        if (this.props.antiForgeryHeaderName != null || this.props.antiForgeryHeaderName != "")
            this.xhr.setRequestHeader(this.props.antiForgeryHeaderName, this.props.antiForgeryToken);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            SuggestionControl.writeDebug(this.props.debug, "xhr onload returned " + this.xhr.responseText);
            this.listIsActive = false;
            // If all good
            if (this.xhr.status === 200) {
                var httpResultData = JSON.parse(this.xhr.responseText); // could also go through event
                if (httpResultData == null || httpResultData.errorMessage != null) {
                    this.processErrorInSuggestionsSearch(httpResultData.errorMessage, userInputObject);
                }
                else {
                    this.processFoundSuggestions(httpResultData.suggestions, userInputObject)
                }
            }
            // all is bad
            else {
                this.processErrorInSuggestionsSearch(this.xhr.responseText, userInputObject);
            }

            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this); 
        this.xhr.send();
    },

    processErrorInSuggestionsSearch: function(errorMessage, userInputObject) {
        SuggestionControl.writeDebug(this.props.debug, "xhr onload errored out. Error text is probably too long.");
        SuggestionControl.writeDebug(this.props.debug, "errorMessage:" + errorMessage);
        this.setState({
            suggestionsData: [],
            listTop: userInputObject.y - 75,
            listLeft: 70, //userInputObject.x,
            showList: false
        });
    },

    // Handles successful search for suggestions
    processFoundSuggestions: function (suggestionsData, userInputObject) {
        SuggestionControl.writeDebug(this.props.debug, "userInputObject.y = " + userInputObject.y + " userInputObject.x " + userInputObject.x);
        this.setState({
            suggestionsData: suggestionsData,
            listTop: userInputObject.y - 150,
            listLeft: 70, //userInputObject.x,
            showList: true
        });
        if (suggestionsData.length > 0) {
            SuggestionControl.writeDebug(this.props.debug, "starting the tick.");
            this.startTicking();
        }
    },

    // Handle user clicking on the suggestions list. hide it and set the text
    handleListClicked: function (phrase) {
        this.listIsActive = false;
        this.setState({
            showList: false, // hide list
            currentValue: phrase.Phrase // set the value in the text box
        });
        // Let the listeners know that something has changed
        this.notifyListenersOnValueChange(phrase.Phrase);
    },

    // Monitor the list mouse movements
    handleListActive: function(active) {
        this.listIsActive = active;
    },

    // Monitor the textbox navigation keys
    handleTextBoxTabOrEscPressed: function() {
        this.listIsActive = false;
        this.setState({
            showList: false // hide list
        });
    },

    // Fire value change event if anyone is listening
    notifyListenersOnValueChange: function (phrase) {
        if (this.props.onValueChange == null)
            SuggestionControl.writeDebug(this.props.debug, "no onValueChange listener.");
        else
            this.props.onValueChange(phrase);
    },

    // Make sure we let the owner know that textbox got focus
    handleTextBoxGotFocus: function (e) {
        if (this.props.onGotFocus == null) return;
        this.props.onGotFocus();
    },

    render: function () {
        return (
            <div>
                <div className="input-group best-some-padding">
                    <span className="input-group-addon index-page-label" id={this.props.inputGroupId}>{this.props.labelText}</span>

                    <SuggestionTextBox onUserTyping={this.handleUserTyping} textValue={this.state.currentValue}
                                   onTabOrEscPressed={this.handleTextBoxTabOrEscPressed}
                                   focusOnLoad={this.props.focusOnLoad} onGotFocus={this.handleTextBoxGotFocus}
                                   inputGroupId={this.props.inputGroupId} placeHolder={this.props.placeHolder} />
                </div>
                <SuggestionResultList suggestions={this.state.suggestionsData}
                                      listTop={this.state.listTop}
                                      listLeft={this.state.listLeft}
                                      onListClicked={this.handleListClicked}
                                      isVisible={this.state.showList}
                                      onListActive={this.handleListActive} />
            </div>
        );
    }
});
