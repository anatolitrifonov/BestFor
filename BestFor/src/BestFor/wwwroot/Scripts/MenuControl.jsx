var MenuControl = React.createClass({
    render: function () {
        return (
            <div>
                <div>
                    <a href="/home/index">Index</a>&nbsp;&nbsp;
                    <a href="/admin/index">Admin</a>&nbsp;&nbsp;
                    <a href="/tests/index">Tests</a>
                </div>
                <div>
                    <a href="/en-US/home/index">Index American en-US</a>&nbsp;&nbsp;
                    <a href="/en-US/admin/index">Admin American en-US</a>&nbsp;&nbsp;
                    <a href="/en-US/tests/index">Tests American en-US</a>
                </div>
                <div>
                    <a href="/ru-RU/home/index">Index Russian</a>&nbsp;&nbsp;
                    <a href="/ru-RU/admin/index">Admin Russian</a>&nbsp;&nbsp;
                    <a href="/ru-RU/tests/index">Tests Russian</a>
                </div>
            </div>
        );
    }
});