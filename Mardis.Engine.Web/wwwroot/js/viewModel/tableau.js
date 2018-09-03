var viz;

      function initViz1( dashboard) {
          var containerDiv = document.getElementById("vizContainer"),
              url = dashboard,
              options = {
                  hideTabs: true
              };

          if (viz)
          { 
              viz.dispose();
          }
          viz = new tableau.Viz(containerDiv, url, options); 
          // Create a viz object and embed it in the container div.
      }
$("#btnLocalizations").on("click", function () {
    var jsonList = JSON.stringify(selectedItems);
    window.location.href = "/Branch/Localization?input=" + jsonList 
    //$.blockUI({ message: "" });
    //$.ajax({
    //    url: "/Branch/Localization",
    //    type: "get",
    //    data: {
    //        input: 
    //    },
    //    success: function (data) {
            
    //        if (data) {
    //            window.location.href = "/Branch/Localization";
    //        } else {
    //            bootbox.alert("Existío un error, Vuelva a intentarlo");
    //        }
    //        $.unblockUI();
    //    },
    //    error: function (xhr) {
    //        console.log(xhr);
    //        $.unblockUI();
    //        //Do Something to handle error
    //    }
    //});
      });

