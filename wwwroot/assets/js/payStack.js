function payWithPaystack() {
    var data = document.getElementById("edit-data-is-correct").checked;
    var email = document.getElementById("id_email").value;
    var amount = document.getElementById("id_amount").value;
    var phoneNumber = document.getElementById("id_phoneNumber").value;
    var key = "";
    $.ajax({
        type: "get",
        url: "/Dashboard/GetConfigurationValue",
        data: { "sectionName": "AppSettings", "paramName":"PayStackPublic" },
        success: function (parameterValue) {
            var json = JSON.stringify(parameterValue);
            var obj = JSON.parse(json);
            key = obj.parameter;
            if (key === "" || key === null)
                return alert('Invalid Paystack Key passed');
            if (!data)
                return alert('Please make sure you check all boxes required');
            var data1 = document.getElementById("edit-term-and-condition").checked;
            if (!data1)
                return alert('Please make sure you check all boxes required');
            var handler = PaystackPop.setup({
                key: key, //put your public key here
                email: email, //put your customer's email here
                amount: amount,
                currency: "NGN",
                metadata: {
                    custom_fields: [
                        {
                            display_name: "Mobile Number",
                            variable_name: "mobile_number",
                            value: phoneNumber//customer's mobile number
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
                            if (data) {
                                alert('Transaction was successful');
                                window.location.href = "/Dashboard/all_companies";
                            }
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
            handler.openIframe(); 
                //key = parameterValue;
        }
    });
    //open the paystack's payment modal
}