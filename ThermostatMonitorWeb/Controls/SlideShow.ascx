<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SlideShow.ascx.cs" Inherits="Controls_SlideShow" %>
<div class="slideshow" id="flavor_1"></div>

<script>
    $.getJSON("/slides/slideshow_data.htm", function(data) {
        $(document).ready(function(){
            $("#flavor_1").agile_carousel({
                carousel_data: data,
                carousel_outer_height: 248,
                carousel_height: 248,
                slide_height: 250,
                carousel_outer_width: 980,
                slide_width: 980,
                transition_time: 300,
                timer: 6000,
                continuous_scrolling: true,
                control_set_1: "numbered_buttons",
                no_control_set: ""
            });
        });
    });
</script>