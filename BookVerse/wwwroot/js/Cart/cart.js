

var jQueryScript = document.createElement('script');
jQueryScript.setAttribute('src', 'https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js');
document.head.appendChild(jQueryScript);



// add to cart 
$('#addtocart, #Buy').click(function () {
    
let pid = document.querySelector("#productid").value;
let pquantity = document.querySelector('#quantity').value;
    const url = '/Customer/Cart/AddToCart';
    let dbuy;
    if (this.id == 'addtocart') {
        dbuy = null;
    }
    else if (this.id == 'Buy') {
        dbuy = true;
    }

    $.ajax({
        type: "POST",
        url: url,
        data: {
            productid: parseInt(pid),
            quantity: parseInt(pquantity),
            directbuy:dbuy
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

   

}
);

//or direct buy




