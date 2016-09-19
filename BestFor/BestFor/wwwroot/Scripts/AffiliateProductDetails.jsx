// Makes a rest call to api controller loading product details
// And shows the product
var AffiliateProductDetails = React.createClass({
    // built in ability to verify type of each property
    propTypes: {
        // URL to load products
        productsUrl: React.PropTypes.string,
        // Keyword to use while searching for product
        productLeftWord: React.PropTypes.string,
        // Keyword to use while searching for product
        productRightWord: React.PropTypes.string,
        // Keyword to use while searching for product
        productPhrase: React.PropTypes.string,
        // SearchIndex to use while searching for product
        productCategory: React.PropTypes.string,
        // token given by the controller to send back as verification
        antiForgeryToken: React.PropTypes.string,
        // token is expected to be sent in this header
        antiForgeryHeaderName: React.PropTypes.string,
        // resource strings are given by the user in this design. Used for displaying localized strings. Provided by server side.
        resourceStrings: React.PropTypes.object,
        // produce debugging info?
        debug: React.PropTypes.bool
    },

    // built in ablity to set initial state
    // Invoked once before the component is mounted. The return value will be used as the initial value of this.state.
    getInitialState: function () {
        return {
            isVisible: false, // Are we shown on load?
            isCouldNotFindVisible: false
        };
    },

    // Static functions that are ... well just static functions ... they can not access state
    statics: {
        writeDebug(debug, message) {
            if (debug) console.log("SuggestionPanel: " + message);
        }
    },

    // Built in event that fires when component is first mounted.
    componentDidMount: function () {
        AffiliateProductDetails.writeDebug(this.props.debug, "AffiliateProductDetails control: launching product search.");
        
        // No point in doing anything if answer URL was not given or the product keyword
        if (this.props.productsUrl === null || this.props.productsUrl.trim() === "") return null;
        if (this.props.productPhrase === null || this.props.productPhrase.trim() === "") return null;
        
        // We are going to handle only one request at a time. Check if there is a request is process already.
        // Will not do anything if xht is not done. Could be anything but as we said only one at a time.
        // In this particular case I doubt we can ever run into the situation like this since answer is fixed and this fires once on load
        // But just in case ... to be safe ...
        if (this.xhr != null && this.xhr.readyState != 4) {
            AffiliateProductDetails.writeDebug(this.props.debug, "AffiliateProductDetails xhr is busy with state " + this.xhr.readyState +
                " " + AffiliateProductDetails.readyStateToText(this.xhr.readyState) + ". Will not do the search.");
            return;
        } else {
            AffiliateProductDetails.writeDebug(this.props.debug, "AffiliateProductDetails xhr is free. Will start searching for products.")
        }

        // lets do a bit of manipulation
        // take only three words from the phrase
        var result = this.props.productPhrase.trim().split(" ");
        var keywords = "";
        for (var i = 0; i < result.length; i++) {
            if (i < 3) keywords += result[i] + " ";
        }
        //keywords = keywords.trim() + " " + this.props.productLeftWord;
        //keywords = keywords.trim() + " " + this.props.productRightWord;
        keywords = keywords.trim();
        AffiliateProductDetails.writeDebug(this.props.debug, "sending " + keywords);

        var url = this.props.productsUrl + "?keyword=" + keywords + "&category=" + this.props.productCategory;
        if (this.xhr == null) this.xhr = new XMLHttpRequest();
        this.xhr.open("get", url, true);
        // add header for antiforgery validation if header was set as a property
        if (this.props.antiForgeryHeaderName != null || this.props.antiForgeryHeaderName != "")
            this.xhr.setRequestHeader(this.props.antiForgeryHeaderName, this.props.antiForgeryToken);
        // handle received data.
        this.xhr.onload = function (e) { // e is of type XMLHttpRequestProgressEvent
            // if all good
            if (this.xhr.status === 200) {
                AffiliateProductDetails.writeDebug(this.props.debug, "AffiliateProductDetails xhr onload returned " + this.xhr.responseText);
                var product = JSON.parse(this.xhr.responseText);
                // we will get null if we were not able to parse
                // ErrorMessage will be set since AffiliateProductDto inherits ErrorMessageDto
                if (product == null || product.ErrorMessage != null) {
                    this.processErrorInProduct(httpResultData.errorMessage);
                }
                else {
                    this.processFoundProduct(product);
                }
            }
            // all is bad. Just FYI: code 204 meas no content.
            else {
                AffiliateProductDetails.writeDebug(this.props.debug, "AffiliateProductDetails ERROR!!!!!! xhr onload returned " + this.xhr.responseText);
                this.processErrorInProduct(this.xhr.responseText);
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

        // Loop through all descriptions returned by the service and combine them into one string.
        var productDescription = "";
        if (product.descriptions != null) {
            var d = product.descriptions;
            var keys = Object.keys(d);
            AffiliateProductDetails.writeDebug(this.props.debug, keys);
            for (var i = 0; i < keys.length; i++) {
                if (productDescription.length > 0) productDescription += " ";
                // productDescription = keys[i] + ":" + d[keys[i]];
                productDescription = d[keys[i]];
            }
        }

        // this is expected to update the list that is bound to this state data.
        this.setState({
            isVisible: true,
            isCouldNotFindVisible: false,
            productTitle: product.title,
            productLink: product.detailPageURL,
            productImageUrl: product.middleImageURL,
            productImageWidth: product.middleImageWidth,
            productImageHeight: product.middleImageHeight,
            productFormattedPrice: product.formattedPrice,
            productDescription: productDescription
        });
    },

    processErrorInProduct: function (errorMessage) {
        AffiliateProductDetails.writeDebug(this.props.debug, "AffiliateProductDetail xhr onload errored out. Error text is probably too long.");
        this.setState({
            isVisible: false,
            isCouldNotFindVisible: true
        });
    },

    // Builds a style object for pop up div
    getOverallDivStyle: function () {
        return {
            display: this.state.isVisible ? "" : "none"
        };
    },

    getCouldNotFindDivStyle: function () {
        return {
            display: this.state.isCouldNotFindVisible ? "" : "none"
        };
    },

    getLinkImageStyle: function () {
        return {
            border: "none"
        };
    },

    // We trust the html that comes from amazon
    rawMarkup: function () {
        if (this.state.productDescription)
            return { __html: this.state.productDescription.toString() };
        return null;
    },

    render: function () {
        return (
            <div style={this.getOverallDivStyle()}>
                <b>{this.props.resourceStrings.found_useful_product}</b><br /><br />
                <b>{this.props.resourceStrings.title_upper}:</b>&nbsp;<a href={this.state.productLink} target="_blank">{this.state.productTitle}</a><br />
                <b>{this.props.resourceStrings.price_upper}:</b>&nbsp;{this.state.productFormattedPrice}<br />
                <div dangerouslySetInnerHTML={this.rawMarkup()} />
                <a href={this.state.productLink} target="_blank">
                    <img width={this.state.productImageWidth} height={this.state.productImageHeight}
                         src={this.state.productImageUrl} style={this.getLinkImageStyle()} title={this.state.productTitle} />
                </a>
                <div style={this.getCouldNotFindDivStyle()}>
                    {this.props.resourceStrings.not_able_to_find_product}
                </div>
            </div>
        );
    }
});
