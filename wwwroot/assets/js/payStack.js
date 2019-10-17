function payWithPaystack() {

    var handler = PaystackPop.setup({
        key: 'pk_test_388f35589b6fa63e562b8910782758ce72da7f6f', //put your public key here
        email: 'ademolajhon@gmail.com', //put your customer's email here
        amount: 10000,
        currency: "NGN",
        metadata: {
            custom_fields: [
                {
                    display_name: "Mobile Number",
                    variable_name: "mobile_number",
                    value: "08134734540" //customer's mobile number
                }
            ]
        },
        callback: function (response) {
            //after the transaction have been completed
            //make post call  to the server with to verify payment
            //using transaction reference as post data
            $.ajax({
                type: "post",
                url: "/Dashboard/Verify_PayStack",
                data: { "reference": response.reference },
                success: function (data) {
                    if (data)
                        alert('Transaction was successful');
                    else {
                        alert(response);

                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        },
        onClose: function () {
            //when the user close the payment modal
            alert('Transaction cancelled');
        }
    });
    handler.openIframe(); //open the paystack's payment modal
}