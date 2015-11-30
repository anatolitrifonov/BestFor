// Displays list of items people suggested already
// Captures onClick from each item and bubbles it up as onListClicked event
var SuggestionAnswerList = React.createClass({
    // built in ability to verify type of each property not required but very useful
    propTypes: {
        onListClicked: React.PropTypes.func    // onListClicked event handler functon
    },

    // Handle click event from an item
    handleItemClicked: function (phrase) {
        // bubble up passing the phrase
        if (this.props.onListClicked != null) this.props.onListClicked({ Phrase: phrase });
        console.log("SuggestionAnswerList handleItemClicked: function (" + phrase + ")");
    },

    // Builds a style object for the div containing list of items
    getListDivStyle: function() {
        return {
            border: "1px solid #ddd"
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

    render: function () {
        var someArray = this.props.answers.map(function (suggestion, i) {
            return (
                <SuggestionLineItem phrase={suggestion.Phrase}
                                    onItemClicked={this.handleItemClicked.bind(this, suggestion.Phrase)} key={i}></SuggestionLineItem>
            );
    }, this);
        
    // render list of the top answers for the pair
    return (
        <div style={this.getListDivStyle()}>
            <ul style={this.getUnsortedListStyle()}>
            {someArray}      
            </ul>
        </div>
        );
    }
});