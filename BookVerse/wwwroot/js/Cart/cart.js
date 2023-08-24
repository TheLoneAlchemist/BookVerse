
$("#addtocart").click(function () {

    const url = '/Customer/Cart/AddToCart'
    let pid = document.querySelector("#productid").value;
    let pquantity = document.querySelector("#quantity").value;

    console.log(pid);
    

        $.ajax({
            type: "POST",
            url: url,
            data: { productid: parseInt(pid), quantity: parseInt(pquantity) },
            success: function (dataCheck) {
                console.log(dataCheck); // <==============================
                if (dataCheck == 'value') {
                    //Do stuff
                }
            }
        });
        return false;
    });

