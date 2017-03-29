/*------------------------------------------------------------------
	[Fucntion]
	
	* Menu Left Background
	* Google Map
	
	* Document Scroll
	
	* Document Ready
		+ Scrolling Navigation
		+ Find all anchors
		+ Add Easing Effect
		+ Responsive Caret	
		+ Menu Left Background
		+ Deal Section
		+ Shoping Brands
		+ Popular Category
		+ ShopTrading
		+ LatestBlog
		+ GetOffers
		+ Counter Section
		+ Featured SignUp
		+ Browse Category
		+ Error Page
		+ Blog
		+ Contact Section
		+ Contact Map
		+ Contact Form

	* Window Load
		+ Site Loader
------------------------------------------------------------------*/

(function($) {

	"use strict"
	
	/* * Menu Left Background */
	function menu_leftbg(){
		var width =	$(window).width();
		var container_width = $(".container").width();
		var menu_left_bg = (( width - container_width ) / 2 );
		$(".menu-left-bg").css("width",menu_left_bg);
	}
	
	/* * Google Map */
	function initialize(obj) {
		var lat = $('#'+obj).attr("data-lat");
        var lng = $('#'+obj).attr("data-lng");
		var contentString = $('#'+obj).attr("data-string");
		var myLatlng = new google.maps.LatLng(lat,lng);
		var map, marker, infowindow;
		var image = "images/marker.png";
		var zoomLevel = parseInt($('#'+obj).attr("data-zoom") ,10);
		var styles = [{"featureType":"landscape","stylers":[{"saturation":" "},{"lightness":" "},{"visibility":"on"}]},{"featureType":"poi","stylers":[{"saturation":" "},{"lightness":" "},{"visibility":"simplified"}]},{"featureType":"road.highway","stylers":[{"saturation":" "},{"visibility":"simplified"}]},{"featureType":"road.arterial","stylers":[{"saturation":" "},{"lightness":" "},{"visibility":"on"}]},{"featureType":"road.local","stylers":[{"saturation":" "},{"lightness":" "},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":" "},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":" "},{"saturation":" "}]},{"featureType":"water","elementType":"geometry","stylers":[{"hue":"#ffff00"},{"lightness":" "},{"saturation":" "}]}]
		var styledMap = new google.maps.StyledMapType(styles,{name: "Styled Map"});	
		var mapOptions = {
			zoom: zoomLevel,
			disableDefaultUI: true,
			center: myLatlng,
            scrollwheel: false,
			mapTypeControlOptions: {
            mapTypeIds: [google.maps.MapTypeId.ROADMAP, "map_style"]
			}
		}
		map = new google.maps.Map(document.getElementById(obj), mapOptions);
		map.mapTypes.set("map_style", styledMap);
		map.setMapTypeId("map_style");
		
		infowindow = new google.maps.InfoWindow({
			content: contentString
		});      
	    
        marker = new google.maps.Marker({
			position: myLatlng,
			map: map,
			icon: image
		});
		google.maps.event.addListener(marker, "click", function() {
			infowindow.open(map,marker);
		});	
	}
	
	/* * Document Scroll - Window Scroll */
	$( document ).scroll(function()
	{
		var scroll	=	$(window).scrollTop();
		var height	=	$(window).height();

		/* set sticky menu */
		if( scroll >= height )
		{
			$(".menu-block").addClass("navbar-fixed-top animated fadeInDown").delay( 2000 ).fadeIn();
		}
		else if ( scroll <= height )
		{
			$(".menu-block").removeClass("navbar-fixed-top animated fadeInDown");
		}
		else
		{
			$(".menu-block").removeClass("navbar-fixed-top animated fadeInDown");
		} 

		if ($(this).scrollTop() >= 50)
		{	
			/* If page is scrolled more than 50px */
			$("#back-to-top").fadeIn(200); /* Fade in the arrow */
		}
		else
		{
			$("#back-to-top").fadeOut(200); /* Else fade out the arrow */
		}
	});
		
	/* * Document Ready - Handler for .ready() called */
	$(document).ready(function($) {
		/* + Scrolling Navigation */
		var scroll	=	$(window).scrollTop();
		var width	=	$(window).width();
		var height	=	$(window).height();
		
		/* set sticky menu */
		if( scroll >= height -500 )
		{
			$(".menu-block").addClass("navbar-fixed-top").delay( 2000 ).fadeIn();
		}
		else if ( scroll <= height )
		{
			$(".menu-block").removeClass("navbar-fixed-top");
		}
		else
		{
			$(".menu-block").removeClass("navbar-fixed-top");
		} /* set sticky menu - end */
		
		/* local url of page (minus any hash, but including any potential query string) */
		var url = location.href.replace(/#.*/,'');

		/* + Find all anchors */
		$("#navbar").find("a[href]").each(function(i,a) {

			var $a = $(a);
			var href = $a.attr("href");

			/* check is anchor href starts with page's URI */
			if ( href.indexOf(url+'#') == 0 ) {

				/* remove URI from href */
				href = href.replace(url,'');

				/* update anchors HREF with new one */
				$a.attr("href",href);
			}
		});

		/* + Add Easing Effect on Section Scroll */
		$('.navbar-nav li a[href*=#]:not([href=#]), .site-logo a[href*=#]:not([href=#])').on('click', function() {

		   if ( location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname ) {
				var target = $(this.hash);
				target = target.length ? target : $('[name=' + this.hash.slice(1) +']');

				if (target.length) {
					$('html, body').animate( { scrollTop: target.offset().top - 83 }, 1000, 'easeInOutExpo' );
					return false;
				}
			}
		});	
		
		/* + Responsive Caret */
		$(".ddl-switch").on("click", function() {

			var li = $(this).parent();
			if ( li.hasClass("ddl-active") || li.find(".ddl-active").length !== 0 || li.find(".dropdown-menu").is(":visible") ) {
				li.removeClass("ddl-active");
				li.children().find(".ddl-active").removeClass("ddl-active");
				li.children(".dropdown-menu").slideUp();
			}
			else {
				li.addClass("ddl-active");
				li.children(".dropdown-menu").slideDown();
			}
		});
		
		/* + Menu Left Background */
		menu_leftbg();
		
		/* + Deal Section  */
		if( $(".deal-section").length ) {
			$(".deal-section").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".deal-section").addClass( "animated fadeInLeft" );
				});
			});
		}
		
		/* + Shoping Brands  */
		if( $(".shopingbrands").length ) {
			$(".shopingbrands").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".shopingbrands").addClass( "animated fadeInRight" );
				});
			});
		}
		
		/* + Popular Category  */
		if( $(".popularcategory").length ) {
			$(".popularcategory").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".popularcategory").addClass( "animated fadeInUp" );
				});
			});
		}
		
		/* + ShopTrading  */
		if( $(".shoptrading").length ) {
			$(".shoptrading-carousel").owlCarousel({
				loop: true,
				/*loop: $("#shoptrading-carousel > .item").length <= 4 ? false : true,*/
				nav: true,
				dots: false,
				margin: 30,
				smartSpeed: 200,
				autoplay: false,
				responsive:{
					0:{
						items: 1
					},
					480:{

						items: 2
					},
					768:{

						items: 3
					},
					1200:{
						items: 4
					}
				}
			})
			
			$(".shoptrading").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".shoptrading").addClass( "animated bounceInLeft" );
				});
			});
		}
		
		/* + LatestBlog  */
		if( $(".latestblog").length ) {
			$(".latestblog").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".latestblog").addClass( "animated zoomInRight" );
				});
			});
		}
		
		/* + GetOffers  */
		if( $(".getoffers").length ) {
			$(".getoffers").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".getoffers").addClass( "animated slideInRight" );
				});
			});
		}
		
		/* + Counter Section  */
		if( $(".counter-section").length ) {
			$('.counter-section').each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					var statistics_item_count = 0;
					var statistics_count = 0;
					statistics_item_count = $( "[id*='statistics_count-']" ).length;
					for(var i=1; i<=statistics_item_count; i++) {
						statistics_count = $( "[id*='statistics_count-"+i+"']" ).attr( "data-statistics_percent" );
						$("[id*='statistics_count-"+i+"']").animateNumber({ number: statistics_count }, 2000);
					}
				});
			});
		
			$(".counter-section").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".counter-section").addClass( "animated slideInLeft");
				});
			});
		}
		
		/* + Featured SignUp  */
		if( $(".featuredsignup").length ) {
			$(".featuredsignup").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".featuredsignup .col-md-7").addClass( "animated fadeInLeft");
					$(".featuredsignup .col-md-5").addClass( "animated fadeInRight");
				});
			});
		}
		
		/* + Browse Category  */
		if( $(".browsecategory").length ) {
			$(".browsecategory").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".browsecategory").addClass( "animated fadeInRight" );
				});
			});
		}
		
		/* + Error Page  */
		if( $(".error-page").length ) {
			$(".error-page").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".error-page .col-md-7").addClass( "animated fadeInLeft");
					$(".error-page .col-md-5").addClass( "animated fadeInRight");
				});
			});
		}
		
		/* + Blog  */
		if( $(".blog").length ) {
			$(".blog").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".blog .content-area").addClass( "animated fadeInLeft");
					$(".blog .widget-area").addClass( "animated fadeInRight");
				});
			});
		}
		
		/* + Contact Section  */
		if( $(".contact-section").length ) {
			$(".contact-section").each(function () {
				var $this = $(this);
				var myVal = $(this).data("value");
				$this.appear(function() {
					$(".contact-section .col-md-9").addClass( "animated fadeInLeft");
					$(".contact-section .col-md-3").addClass( "animated fadeInRight");
				});
			});
		}
		
		/* + Contact Map */
		if($("#map-canvas-contact").length==1){
			initialize("map-canvas-contact");
		}
		
		/* + Contact Form */
		$( "#btn_submit" ).on( "click", function(event) {
		  event.preventDefault();
		  var mydata = $("form").serialize();
			$.ajax({
				type: "POST",
				dataType: "json",
				url: "contact.php",
				data: mydata,
				success: function(data) {
					if( data["type"] == "error" ){
						$("#alert-msg").html(data["msg"]);
						$("#alert-msg").removeClass("alert-msg-success");
						$("#alert-msg").addClass("alert-msg-failure");
						$("#alert-msg").show();
					} else {
						$("#alert-msg").html(data["msg"]);
						$("#alert-msg").addClass("alert-msg-success");
						$("#alert-msg").removeClass("alert-msg-failure");
						$("#input_name").val("");
						$("#input_email").val("");
						$("#input_phone").val("");
						$("#input_subject").val("");
						$("#textarea_message").val("");
						$("#alert-msg").show();
					}
				},
				error: function(xhr, textStatus, errorThrown) {
					alert(textStatus);
				}
			});
			return false;
			/*$("#contact-form").attr("action", "saveQuery").submit();*/
		});
		
	});	/* + Document Ready /- */
	
	/* * Window Resize */
	$(window).resize(function() {
		/* + Menu Left Background */
		menu_leftbg();
	});
	
	/* * Window Load - Handler for .load() called */
	$(window).load(function() {
		/* + Site Loader */
		if ( !$("html").is(".ie6, .ie7, .ie8") ) {
			$("#site-loader").delay(1000).fadeOut("slow");
		}
		else {
			$("#site-loader").css("display","none");
		}
	});

})(jQuery);