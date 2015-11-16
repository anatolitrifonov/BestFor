// If you need to interact with the browser, perform your work in componentDidMount() or the other lifecycle methods instead.

/*
Control to show two text box fields search form
State: suggestionsData - last list of suggestions loaded from the server
*/
var TwoWordsForm = React.createClass({
    // built in ability to verify type of each property
    // not required but very useful
    propTypes: {
        // onUserTyping event handler should be a function
        suggestionsUrl: React.PropTypes.string
    },

    // built in ablity to set initial state
    // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
    getInitialState: function () {
        // initial set of suggestions is blank
        return {
            suggestionsData: [],
            listTop: 110,
            listLeft: 20
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

    // handle user typing text in the word box.
    // load data and show in usggestions popup
    handleUserTyping: function (userInputObject) {
        // console.log("user typing was handled");
        // We are going to handle only one request at a time.
        // check if there is a request is process already
        
        // will not do anything if xht is not done. Could be anything but as we said only one at a time.
        if (this.xhr != null && this.xhr.readyState != 4) {
            console.log("xhr is busy with state " + this.xhr.readyState + " " + this.readyStateToText(this.xhr.readyState));
            return;
        }
            
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
                listLeft: userInputObject.x
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
                <SearchTextBox onUserTyping={this.handleUserTyping} />
                <SearchResultList suggestions={this.state.suggestionsData} listTop={this.state.listTop} listLeft={this.state.listLeft} />
            </div>
        );
    }
});

// Displays search result list
// Properties:
//      data - list of items to render - needs to be set by instantiator
var SearchResultList = React.createClass({
    // built in ability to verify type of each property not required but very useful
    propTypes: {
        // onUserTyping event handler should be a function
        listTop: React.PropTypes.number,
        listLeft: React.PropTypes.number
    },

    // built in ablity to set initial state
    getInitialState: function () {
        // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
        return {
            isVisible: true          // Visible or not
        };
    },

    // Handle click event from an item
    handleItemClicked: function (phrase) {
        console.log("handleItemClicked: function (phrase) {");
        // hide the list
        this.setState({ isVisible: false })
    },

    render: function () {
        // only show the first 10 items
        var someArray = this.props.suggestions;
        if (someArray.length > 10) someArray = someArray.slice(0, 10);

        // this.props is built in
        // this.props.suggestions needs to be set by instance creator
        // this.props.suggestions.map
        // this.props is react object. Any property of reach object is a child of this reach object.
        // In this case property "suggestions" is a child
        // React exposes .map for child objects
        // array React.Children.map(object children, function fn [, object thisArg])
        //    Invoke fn on every immediate child contained within children with this set to thisArg.
        //    If children is a nested object or array it will be traversed: fn will never be passed the container objects. 
        //    If children is null or undefined returns null or undefined rather than an array.
        // suggestion would be an element of suggestions
        // We know that it has property Phrase
        // someArray is array of items function is executed for each item in array during rendering
        // without.bind handleItemClicked will not fire
        // ",this" ant the end is part of binding . I am not 100 on how this works.
        someArray = someArray.map(function (suggestion, i) {
            return (
                <SuggestionLineItem phrase={suggestion.Phrase}
                                    onItemClicked={this.handleItemClicked.bind(this, i)} key={i}></SuggestionLineItem>
            );
        }, this);

        var styles = {
            border: "1px solid #ddd",
            position: "absolute",
            backgroundColor: "red",
            zIndex:1000,
            top: this.props.listTop,
            left: this.props.listLeft,
            display: this.state.isVisible ? "" : "none"
        };
        // render list on items in suggestions
        return (
            <div style={styles}>
                <ul>
                {someArray}      
                </ul>
            </div>
        );
    }
});