// Displays list of items people suggested already
// Captures onClick from each item and bubbles it up as onListClicked event
var SuggestionAnswerList = React.createClass({
    // Built in ability to verify type of each property not required but very useful
    propTypes: {
        onListClicked: React.PropTypes.func,    // onListClicked event handler functon
        culture: React.PropTypes.string         // culture for building links
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
            border: "0px solid #ddd",
            width: 500
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
        var someArray = this.props.answers.map(function (answer, i) {
            return (
                <SuggestionAnswerItem phrase={answer.phrase} count={answer.count}
                        onItemClicked={this.handleItemClicked.bind(this, answer.phrase)} key={i}
                        culture={this.props.culture}
                        answerId={answer.id}></SuggestionAnswerItem>
            );
        }, this);
        
        // render list of the top answers for the pair
        // div used to be filled with ul
        return (
            <div className="best-div-fluid">
                {someArray}      
            </div>
        );
    }
});