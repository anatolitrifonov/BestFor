var SuggestionPanel = React.createClass({
    // built in ability to verify type of each property
    propTypes: {
        // not required but very useful
        // onUserTyping event handler should be a function
        suggestionsUrl: React.PropTypes.string
    },
  //          @Html.React("SuggestionControl", new { suggestionsUrl = "/api/suggestion" }, "span")
//        @Html.React("SuggestionControl", new { suggestionsUrl = "/api/suggestion" }, "span")

    render: function () {
        return (
            <div>
                <SuggestionControl />
                <SuggestionControl />
                <SuggestionAnswerList />
            </div>
        );
    }
});
