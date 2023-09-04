

var jQueryScript = document.createElement('script');
jQueryScript.setAttribute('src', 'https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js');
document.head.appendChild(jQueryScript);
$("#addtocart").click(function () {
    const url = '/Customer/Cart/AddToCart';
    let pid = document.querySelector("#productid").value;
    let pquantity = document.querySelector('#quantity').value;

    $.ajax({
        type: "POST",
        url: url,
        data: {
            productid: parseInt(pid),
            quantity: parseInt(pquantity)
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

