$(window).bind("load", function() {
   resizecontainer();
    $(".msg").focus();
});

$(window).resize(function() {
   resizecontainer();
});

$("#upload_link").click(function(e){
e.preventDefault();
$("#upload").trigger('click');
});

function myFunction(x) {
x.classList.toggle("change");
}
$(document).on("focus keyup keydown",".msg",function(){
var rows = document.querySelector('textarea').value.split("\n").length;
    if(rows < 2){
    //    console.log(rows);
    $(".msg").css("height","20px");
    }
    else{
    $(".msg").attr("rows",rows);
    $(".msg").css("height","auto");
    resizecontainer();
    }
});

$(document).on("keyup keydown",".msg",function(){
    if($(this).val().length > 0){
    typestart();
        resizecontainer();
}
    else{
    typestop();
    }
});

$(document).on("focusout",".msg",function(){
typestop();
});

function typestart(){
$(".typing").fadeIn(100);
}
function typestop(){
$(".typing").fadeOut(100);
}

//document.getElementById('upload').onchange = function () {
//var triple = $('#upload').val();
//var bogus = triple.split("\\");
//alert("File name is "+bogus[bogus.length - 1]);
//};

function resizecontainer(){
//    var bodyheight = $("body").height();
//    var headerheight = 56; // $(".mdl-layout__header-row").height();
//    var footerheight =  $(".mdl-mini-footer").height();
//    var headandfoot = headerheight + footerheight;
//    var res = (bodyheight - headandfoot) - 20;
//    $(".mdl-layout__content .chat-container").css("height", res+"px");
    var bodyheight = $("body").height();
    var footerheight =  $(".mdl-mini-footer").height();
    var res = (bodyheight - footerheight);
   // if($(".mdl-layout__content").height() > 200){
    $(".mdl-layout__content").css("height", res+"px");
    $(".mdl-layout__content").css("bottom", footerheight+"px");
   //}
}

bajb_backdetect.OnBack = function()
{
alert('You clicked it!');
}