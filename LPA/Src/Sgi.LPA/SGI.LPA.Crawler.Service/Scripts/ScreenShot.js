// Read the Phantom webpage '#intro' element text using jQuery and "includeJs"

 "use strict";
var page = require('webpage').create();
var system = require('system');
var address, selector, filePath

page.onConsoleMessage = function(msg) {
    console.log(msg);
};

if (system.args.length < 1) {
    console.log('No arguments provided');
    phantom.exit();
} else {
	address = system.args[1];
	selector = system.args[2];
	filePath = system.args[3];
	
	page.open(address, function(status) {
		if (status === "success") {  
			page.includeJs("http://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js", function() {
				var content = evaluate(page,function(selector) {	
					var position = $(selector).offset();
					var width = $(selector).width();
					var height = $(selector).height();
					return {"position": position, "eleWidth": width, "eleHeight": height};
		
				},selector);
					
				page.clipRect = { top: content.position.top, left: content.position.left, width: content.eleWidth, height: content.eleHeight };
				page.render(filePath);
					
				phantom.exit(0);
			});
		} else {
		  phantom.exit(1);
		}
	});
}


function evaluate(page, func) {
    var args = [].slice.call(arguments, 2);
    var fn = "function() { return (" + func.toString() + ").apply(this, " + JSON.stringify(args) + ");}";
    return page.evaluate(fn);
}