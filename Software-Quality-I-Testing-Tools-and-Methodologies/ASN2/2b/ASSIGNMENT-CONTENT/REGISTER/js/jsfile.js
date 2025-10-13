
var errorMessages = "";


function ValidateInputs() {

    var f = $("#registrationForm");

    f.validate({
        rules: {
            firstName: {
                required: true
            },
            lastName: {
                required: true
            },
            address: {
                required: true
            },
            city: {
                required: true
            },
            province: {
                required: false
            },
            postalCode: {
                required: true,
                postalCodeCheck: true
            },
            phone: {
                required: true,
                phoneNumberCheck: true
            },
            email: {
                required: true
            },
            numberOfParticipants: {
                required: true,
                min: 1
            },
            daysRegistered: {
                required: true
            }
        },
        messages: {
            firstName: {
                required: "First Name is required"
            },
            lastName: {
                required: "Last Name is required"
            },
            address: {
                required: "Address is required"
            },
            city: {
                required: "City is required"
            },
            province: {
                required: "City is required"
            },
            postalCode: {
                required: "Postal Code is required",
                postalCodeCheck: "Postal Code must follow the pattern A1A 1A1"
            },
            phone: {
                required: "Phone is required",
                phoneNumberCheck: "Phone Number must follow the patterns 111-111-1111 or (111)111-1111"
            },
            email: {
                required: "email is required"

            },
            numberOfParticipants: {
                required: "Please provide the number of participants to be registered",
                min: "Minimum number of participants is 1"
            },
            daysRegistered: {
                required: "Please provide the number of day(s) are you registering for"
            }
        }
    });

    return f.valid();

}


jQuery.validator.addMethod("postalCodeCheck",
    function (value, element) {
        var postalCodePattern = /^[A-Z][0-9][A-Z]\s[0-9][A-Z][0-9]$/i;

        return this.optional(element) || postalCodePattern.test(value);

    },
    "Postal Code must follow the pattern A1A 1A1"
);

jQuery.validator.addMethod("phoneNumberCheck",
    function (value, element) {
        var phonePattern1 = /^\d{3}-\d{3}-\d{4}$/;
        
        return this.optional(element) || phonePattern1.test(value);

    },
    "Phone Number must follow the patterns 111-111-1111 or (111)111-1111"
);


function Register() {
    if (ValidateInputs()) {

        var json = {};

        $(":input").each(function () {
            json[$(this).attr("id")] = $(this).val();
        });

        localStorage.setItem("registration", JSON.stringify(json));
        // localStorage.setItem("registration-" + nextId, JSON.stringify(json));
        // localStorage.setItem("nextId", String(nextId + 1));
        $(location).prop('href', 'viewRegistration.html');

    }
}

function LoadRegistrationData(id) {

    var json = JSON.parse(localStorage.getItem("registration"));

    $(":input").each(function () {
        $(this).val(json[$(this).attr("id")]);
    });

    console.log(json);

    CalculatePrice();

}

function CalculatePrice(){

    var json = JSON.parse(localStorage.getItem("registration"));

    var numberOfParticipants = Number(json['numberOfParticipants']);
    var dayOption = json['daysRegistered'];
    var price;

    if(dayOption === "day1"){
        price = 350;

    }
    else if(dayOption === "day2"){
        price = 450;

    }
    else {
        price = 7500;
    }

    if(numberOfParticipants > 5){
        price = price * 0.90;
    }

    price = price * numberOfParticipants;


    console.log(price);

    $("#price").val("$" + price + ".67");

}


