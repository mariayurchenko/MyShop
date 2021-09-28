function Clear(executionContext) {
    console.log("Clear");
    var formContext = executionContext.getFormContext();
    formContext.getControl("mobilephone").clearNotification();
}

function ValidatePhoneNumber(executionContext) {
    var id = window.Xrm.Page.data.entity.getId();

    var errorId = "error";
    var formContext = executionContext.getFormContext();
    formContext.getControl("mobilephone").clearNotification();
    var mobileNumber = formContext.getAttribute("mobilephone").getValue();

    if (mobileNumber) {
        mobileNumber = new String(mobileNumber).replace(/[^\d]+/g, "");
        var pattern = "^\\d{10}$";

        if (mobileNumber.length === 12) {
            if (mobileNumber.substr(0, 2) === "38") {
                mobileNumber = mobileNumber.substr(2);
            }
            else {
                formContext.getControl("mobilephone").setNotification("Please Enter Ukraine's Mobile Number.");
            }
        }

        if (mobileNumber.match(pattern)) {
            var phoneFormatted = mobileNumber.replace(/(\d{3})(\d{3})(\d{4})/, "+38 ($1) $2-$3");
            var setValue = false;
            var fullname;
            if (id) {
                Xrm.WebApi.online.retrieveRecord("contact", id, "?$select=fullname").then(
                    function success(result) {
                         fullname = result["fullname"];
                        console.log(fullname);
     
                    },
                    function (error) {
                        Xrm.Utility.alertDialog(error.message);
                    }
                );
            }

            Xrm.WebApi.online.retrieveMultipleRecords("contact", `?$filter=mobilephone eq '${mobileNumber}'`).then(
                function success(results) {
                  
                    if (results.entities.length === 0) {
                        setValue = true;
                    }
                    else {
                        console.log(results);
                        for (var i = 0; i < results.entities.length; i++) {
                            console.log(results.entities[i].fullname);
                            if (fullname === results.entities[i].fullname) {
                                setValue = true;
                                console.log("yes");
                            }
                        }
                    }
                    console.log(setValue);
                    console.log(mobileNumber);
                    console.log(phoneFormatted);
                    if (setValue) {
                        formContext.getAttribute("mobilephone").setValue(phoneFormatted);
                    }
                    else {
                        formContext.getControl("mobilephone").setNotification('This Mobile Number already used.');
                    }
                },
                function (error) {
                    formContext.getControl("mobilephone").setNotification(error.message);
                }
            );
        }
        else {
            formContext.getControl("mobilephone").setNotification('Please Enter Valid Mobile Number.');
        }
    }
}