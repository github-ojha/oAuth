(function ($) {
    $('.ui.form').form({
        revUrl: {
            identifier: 'revurl',
            rules: [{
                type: 'empty',
                prompt: 'Please enter rev url'
            }]
        },
        apiKey: {
            identifier: 'apikey',
            rules: [{
                type: 'empty',
                prompt: 'Please enter api key'
            }]
        },
        apiSecret: {
            identifier: 'apisecret',
            rules: [{
                type: 'empty',
                prompt: 'Please enter api secret'
            }]
        }        
    }, {
        on: 'blur',
        inline: 'true'
    });
}(jQuery));