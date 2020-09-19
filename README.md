# Twilio Authy

Common operations of Twilio Authy like create new users, send OTP to user, verify OTP, delete user


# Installation

install nuget package tomal-hossain.TwilioAuthy


# User Module

Create Authy instance using your production api key of Twilio.
Example: Authy authy = new Authy("production_api_key");


# Create User in Twilio

You need to pass email, phone number and country code to create user in Twilio.
Example: int authyId = await authy.CreateUser(email, phone, country_code)
For successfull API call it will return a unique authy_id of type integer. Save it to your database for further use. It will return 0 if fails to create user.


# Send OTP to user

You need to pass the authy id to send otp.
Example: bool status = authy.SendOTP(authy_id);
For successfull API call it will return true and send a 6-8 digit token to user phone, or it will return false.


# Verify OTP

You need to pass the authy_id and token to verify it.
Example: bool status = authy.VerifyOTP(authy_id, token);
It will return true if successfully verified or return false.

# Remove User

You need to pass the authy_id to remove the user from Twilio.
Example: bool status = authy.RemoveUser(authy_id);
It will return true if successfully removed or return false.
