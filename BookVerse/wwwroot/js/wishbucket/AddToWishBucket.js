var jQueryScript = document.createElement('script');
jQueryScript.setAttribute('src', 'https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js');
document.head.appendChild(jQueryScript);


$(".addtowish").click((e) => {
    
    console.log(e.target.value) 
    const url = '/Customer/WishList/AddtoWish';

    let pid = e.target.value;


    $.ajax({
        type: "POST",
        url: url,
        data: {
            productId: pid
            
        },
        // The expected data type of the response
        success: function (response) {
            console.log("Success! Response: " + JSON.stringify(response));
            if (response["success"]) {
                toastr.success(response["success"]);
            }
            if (response["error"]) {

                toastr.error(response["error"]);
            }
            if (response['url']) {

                window.location.href = response['url'];
            }
        },
        error: function (xhr, status, error) {
            //console.log("Error: " + error.toString());
            //console.log("Xhr: " + xhr);
            //console.log("status: " + status);
            if (xhr.status === 401) {
                window.location.href = '/Identity/Account/Login';
            }
        }
    });



});