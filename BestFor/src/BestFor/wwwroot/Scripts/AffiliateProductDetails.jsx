// Makes a rest call to api controller loading product details
var AffiliateProductDetails = React.createClass({
    // built in ability to verify type of each property
    propTypes: {
        // URL to load products
        productsUrl: React.PropTypes.string,
        // Keyword to use while searching for product
        productKeyword: React.PropTypes.string,
        // token given by the controller to send back as verification
        antiForgeryToken: React.PropTypes.string,
        // token is expected to be sent in this header
        antiForgeryHeaderName: React.PropTypes.string,
        // resource strings are given by the user in this design. Used for displaying localized strings. Provided by server side.
        resourceStrings: React.PropTypes.object
    },

    // built in ablity to set initial state
    // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
    getInitialState: function () {
        return {
            isVisible: true          // Are we shown on load? Should be no, but setting to yes for now for debugging.
        };
    },

    // Built in event that fires when component is first mounted.
    componentDidMount: function () {
        console.log("AffiliateProductDetails control: launching product search.");
        
        // No point in doing anything if answer URL was not given or the product keyword
        if (this.props.productsUrl === null || this.props.productsUrl.trim() === "") return null;
        if (this.props.productKeyword === null || this.props.productKeyword.trim() === "") return null;
        
        // We are going to handle only one request at a time. Check if there is a request is process already.
        // Will not do anything if xht is not done. Could be anything but as we said only one at a time.
        // In this particular case I doubt we can ever run into the situation like this since answer is fixed and this fires once on load
        // But just in case ... to be safe ...
        if (this.xhr != null && this.xhr.readyState != 4) {
            console.log("AffiliateProductDetails xhr is busy with state " + this.xhr.readyState +
                " " + AffiliateProductDetails.readyStateToText(this.xhr.readyState) + ". Will not do the search.");
            return;
        } else {
            console.log("AffiliateProductDetails xhr is free. Will start searching for products.")
        }

        var url = this.props.productsUrl + "?keyword=" + this.props.productKeyword;
        if (this.xhr == null) this.xhr = new XMLHttpRequest();
        this.xhr.open("get", url, true);
        // add header for antiforgery validation if header was set as a property
        if (this.props.antiForgeryHeaderName != null || this.props.antiForgeryHeaderName != "")
            this.xhr.setRequestHeader(this.props.antiForgeryHeaderName, this.props.antiForgeryToken);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            // if all good
            if (this.xhr.status === 200) {
                console.log("AffiliateProductDetails xhr onload returned " + this.xhr.responseText);
                var product = JSON.parse(this.xhr.responseText);
                //if (product == null || httpResultData.ErrorMessage != null) {
                //    this.processErrorInAnswersSearch(httpResultData.ErrorMessage);
                //}
                //else {
                this.processFoundProduct(product);
                //}
            }
            // all is bad. Just FYI: code 204 meas no content.
            else {
                console.log("AffiliateProductDetails ERROR!!!!!! xhr onload returned " + this.xhr.responseText);
                // this.processErrorInAnswersSearch(this.xhr.responseText);
            }
            // without this xhr event handler will not have access to this.xhr
            // I guess it bind function to component context
        }.bind(this);
        // this.xhr.onerror -> Chrome does not implement onerror
        this.xhr.send();
    },

    // Handles successful search of product. Show product.
    processFoundProduct: function (product) {
        //var message = this.props.resourceStrings.suggestion_panel_no_answers_found;
        //if (answers != null && answers.length > 0)
        //    message = answers.length + this.props.resourceStrings.suggestion_panel_x_answers_found;
        //// Save the answers to use later
        //this.answers = answers;
        // this is expected to update the list that is bound to this state data.
        this.setState({
            isVisible: true,
            productTitle: product.Title,
            productLink: product.DetailPageURL
        });
    },

    //processErrorInAnswersSearch: function (errorMessage) {
    //    console.log("SuggestionPanel xhr onload errored out. Error text is probably too long.");
    //    this.answers = [];
    //    this.setState({
    //        statusMessage: this.props.resourceStrings.suggestion_panel_error_happened_searching_answers, // message
    //        answers: this.answers, // blank out the aswers
    //        showErrorPane: true, // show errors
    //        errorMessage: errorMessage, // show error details
    //        showAddDescriptionLink: false
    //    });
   // },


    // Builds a style object for pop up div
    getOverallDivStyle: function () {
        return {
            display: this.state.isVisible ? "" : "none"
        };
    },

    render: function () {
        return (
            <div style={this.getOverallDivStyle()}>
                Title:{this.state.productTitle} <br />
                Price: image <br />
                Link: {this.state.productLink} <br />
                Link: link <br />
            </div>
        );
    }
});
