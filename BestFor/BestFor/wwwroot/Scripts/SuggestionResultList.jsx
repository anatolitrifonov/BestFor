// List of SuggestionLineItem components
// Displays search result list of suggestions
// Captures onClick from each item and bubbles it up as onListClicked event
// Parent drives the logic of show or hyde list through showList property because it depends on data being loaded or not and user typing
// or not typing on the list
var SuggestionResultList = React.createClass({
    // built in ability to verify type of each property not required but very useful
    propTypes: {
        listTop: React.PropTypes.number,        // top position of this list
        listLeft: React.PropTypes.number,       // left position of this list
        onListClicked: React.PropTypes.func,    // onListClicked event handler functon
        isVisible: React.PropTypes.bool,        // parent's indication to show or hide the list
        onListActive: React.PropTypes.func      // onListActive event handler functon
    },

    // Handle click event from an item
    handleItemClicked: function (phrase) {
        // bubble up passing the phrase
        if (this.props.onListClicked != null) this.props.onListClicked({ Phrase: phrase });
        console.log("SuggestionResultList handleItemClicked: function (" + phrase + ")");
    },

    // Builds a style object for pop up div
    getPopUpDivStyle: function() {
        return {
            border: "1px solid #ddd",
            position: "absolute",
            backgroundColor: "white",
            zIndex: 1000,
            top: this.props.listTop,
            left: this.props.listLeft,
            display: this.props.isVisible && this.isDisplayAbleBasedOnSuggestions ? "" : "none"
        };
    },

    // Builds a style object for unsorted list
    getUnsortedListStyle: function () {
        return {
            padding: 0,
            margin: 0,
            listStyleType: "none",
            textAlign: "left"
        };
    },

    // Is there anything to show?
    isDisplayAbleBasedOnSuggestions: function() {
        return this.props.suggestions.length > 0;
    },

    // Cut list of suggestions to maximum displayable
    getLocalSuggestions: function () {
        var someArray = this.props.suggestions;
        if (someArray.length > 10) someArray = someArray.slice(0, 10);
        return someArray;
    },

    // List is considred active while mouse is moving over it.
    handleMouseOver: function(e) {
        if (this.props.onListActive != null) this.props.onListActive(true);
    },

    // List is considred not active if mouse it out.
    handleMouseOut: function (e) {
        if (this.props.onListActive != null) this.props.onListActive(false);
    },

    render: function () {
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
        var someArray = this.getLocalSuggestions().map(function (suggestion, i) {
            return (
                <SuggestionLineItem phrase={suggestion.phrase}
                                    onItemClicked={this.handleItemClicked.bind(this, suggestion.phrase)} key={i}></SuggestionLineItem>
            );
        }, this);
        
        // render list on items in suggestions
        return (
            <div style={this.getPopUpDivStyle()} onMouseOver={this.handleMouseOver} onMouseOut={this.handleMouseOut} >
                <ul style={this.getUnsortedListStyle()}>
                {someArray}      
                </ul>
            </div>
            );
    }
});