angular
    .module('myApp')
    .controller('homepage',function($scope){
        $scope.$on('$viewContentLoaded', function(){
        resizecontainer();
      });

    //response1
    $scope.response1 =  {
            "title" : "68.43 Indian Rupee",
            "description" : "1 US Dollar",
            "link" : "#",
            "linktext" : "disclaimer"
      };

    //response2
    $scope.response2 =  {
            "title" : "Headgear | Apparel | Google Merchandise Store",
            "description" : "This google Cap is a hat with a twist. Made of a wool-poly blend, this hat works as a baseball cap or a fashion...",
            "mainlink" : "https://shop.googlemerchandisestore.com..",
            "mainlinktext" : "https://shop.googlemerchandisestore.com..",
            "link" : "#",
            "linktext" : "disclaimer"
      };

    //response5
    $scope.totalitem = "4";
    $scope.retailer = [
       {
            "url" : "contents/images/hotel.jpg",
            "name" : "Three star mart",
            "place" : "San Franciso"
        },{
            "url" : "contents/images/hotel1.jpg",
            "name" : "Heb Food Store",
            "place" : "California"
        },{
            "url" : "contents/images/hotel2.jpg",
            "name" : "Valero Corner Store",
            "place" : "Miami"
        },{
            "url" : "contents/images/hotel3.jpg",
            "name" : "7 star Convience Store",
            "place" : "chicago"
        }
    ];
});