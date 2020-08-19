let emailValidationMessages = {email:'Please enter valid email.', 
                                    required:'Email is required.',
                                    match: 'email doesnt match.'};
let mobileValidationMessages = {invalidlength:'Mobile number must be between 10-13 digit.',
                                    required:'Mobile is required.',
                                    notnumber: `Enter valid mobile number.`};

export{emailValidationMessages, mobileValidationMessages};