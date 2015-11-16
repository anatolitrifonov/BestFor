// Simple text box that fires onUserTyping event when text is over 2 letters
// Catches onChange. Fires onUserTyping when over 2 letters
var SearchTextBox = React.createClass({
    // built in ability to verify type of each property
    // not required but very useful
    propTypes: {
        // onUserTyping event handler should be a function
        onUserTyping: React.PropTypes.func
    },

    // Called when user changes the box value without losing focus. Does not fire on losing the focus.
    handleChange: function (e) {
        // as user types we want to load the suggestions once value is more than 2 letters
        if (e == null || e.target == null || e.target.value == null || e.target.value.length < 3) return;
        // is event hadler assigned?
        // can't fire if not assigned. Exception will happen.
        if (this.props.onUserTyping == null) {
            console.debug("SearchTextBox can not fire event. onUserTyping handler is null.");
            return;
        }
        // let's also send position where to show the popup
        var rect = e.target.getBoundingClientRect();

        // let's verify if we want to fire some event but it is not configured in properties
        this.props.onUserTyping({ Phrase: e.target.value, x: rect.left, y: rect.top + rect.height });
    },
    render: function () {
        return (
            <input type="text" placeholder="first word" onChange={this.handleChange} />
        );
    }
});
