// If you need to interact with the browser, perform your work in componentDidMount() or the other lifecycle methods instead.

/* Control to show two text box fields search form */
var TwoWordsForm = React.createClass({
    something: "asd",

    handleUserTyping: function (userInput) {
        console.log("user typing was handled");
        //var comments = this.state.data;
        //var newComments = comments.concat([comment]);
        //this.setState({ data: newComments });

        //var data = new FormData();
        //data.append('Author', comment.Author);
        //data.append('Text', comment.Text);

        //var xhr = new XMLHttpRequest();
        //xhr.open('post', this.props.submitUrl, true);
        //xhr.onload = function () {
        //    this.loadCommentsFromServer();
        //}.bind(this);
        //xhr.send(data);
    },
    render: function () {
        return (
            <div>
            <SearchTextBox onUserTyping={this.handleUserTyping} />
            <SearchAllButton />
            <SearchResultList data={randomSuggestionsData} />
            </div>
        );
    }
});


var SearchAllButton = React.createClass({
    render: function () {
        return(
            <input type="button" value="Search" />
        );
    }
});

var randomSuggestionsData = [
  { Phrase: "Daniel Lo Nigro" },
  { Phrase: "Z" },
  { Phrase: "FGDDD" }
];

// Displays search result list
// Properties:
//      data - list of items to render - needs to be set by instantiator
var SearchResultList = React.createClass({
    render: function () {
        // this.props is built in
        // this.props.data needs to be set by instance creator
        // this.props.data.map
        // this.props is react object. Any property of reach object is a child of this reach object.
        // In this case property "data" is a child
        // React exposes .map for child objects
        // array React.Children.map(object children, function fn [, object thisArg])
        //    Invoke fn on every immediate child contained within children with this set to thisArg.
        //    If children is a nested object or array it will be traversed: fn will never be passed the container objects. 
        //    If children is null or undefined returns null or undefined rather than an array.
        // suggestion would be an element of data
        // We know that it has property Phrase
        // someArray is array of items function is executed for each item in array during rendering
        var someArray = this.props.data.map(function (suggestion) {
            return (
                <span>{suggestion.Phrase}</span>
                );
        });
        // render list on items in data
        return (
            <div>
                {someArray}      
            </div>
        );
    }
});
/*
var CommentForm = React.createClass({
    handleSubmit: function(e) {
        e.preventDefault();
        var author = this.refs.author.getDOMNode().value.trim();
        var text = this.refs.text.getDOMNode().value.trim();
        if (!text || !author) {
            return;
        }
        this.props.onCommentSubmit({Author: author, Text: text});
        // TODO: send request to the server
        this.refs.author.getDOMNode().value = '';
        this.refs.text.getDOMNode().value = '';
        return;
    },
    render: function() {
        return (
            <form className="commentForm" onSubmit={this.handleSubmit}>
                <input type="text" placeholder="Your name" ref="author" />
                <input type="text" placeholder="Say something..." ref="text" />
                <input type="submit" value="Post" />
            </form>
        );
    }
});*/
/*var CommentBox = React.createClass({
    loadCommentsFromServer: function() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    handleCommentSubmit: function(comment) {
        var comments = this.state.data;
        var newComments = comments.concat([comment]);
        this.setState({data: newComments});

        var data = new FormData();
        data.append('Author', comment.Author);
        data.append('Text', comment.Text);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = function() {
            this.loadCommentsFromServer();
        }.bind(this);
        xhr.send(data);
    },
    getInitialState: function() {
        return { data: this.props.initialData };
    },
    componentDidMount: function() {
        this.loadCommentsFromServer();
        window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
    },
    render: function() {
        return (
          <div className="commentBox">
            <h1>Comments</h1>
            <CommentList data={this.state.data} />
            <CommentForm onCommentSubmit={this.handleCommentSubmit} />
          </div>
      );
    }
});
var CommentList = React.createClass({
    render: function() {
        var commentNodes = this.props.data.map(function (comment) {
            return (
              <Comment author={comment.Author}>{comment.Text}
                </Comment>
              );
    });
return (
  <div className="commentList">
    {commentNodes}      
  </div>
    );
}
});

var CommentForm = React.createClass({
    handleSubmit: function(e) {
        e.preventDefault();
        var author = this.refs.author.getDOMNode().value.trim();
        var text = this.refs.text.getDOMNode().value.trim();
        if (!text || !author) {
            return;
        }
        this.props.onCommentSubmit({Author: author, Text: text});
        // TODO: send request to the server
        this.refs.author.getDOMNode().value = '';
        this.refs.text.getDOMNode().value = '';
        return;
    },
    render: function() {
        return (
                            <form className="commentForm" onSubmit={this.handleSubmit}>
          <input type="text" placeholder="Your name" ref="author" />
        <input type="text" placeholder="Say something..." ref="text" />
        <input type="submit" value="Post" />
            </form>
    );
}
});
var Comment = React.createClass({
    render: function() {
        var converter = new Showdown.converter();
        var rawMarkup = converter.makeHtml(this.props.children.toString());
        return (
            <div className="comment">
                <h2 className="commentAuthor">{this.props.author}</h2>
                <span dangerouslySetInnerHTML={{__html: rawMarkup}} />
            </div>
        );
}
});
{
React.render(
  <CommentBox url="/comments" submitUrl="/comments/new" pollInterval={2000} />,
  document.getElementById('content')
);}*/