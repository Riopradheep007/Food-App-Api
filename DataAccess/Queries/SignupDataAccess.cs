using Common.Model.Signup;
using DataAccess.Abstract;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess.Queries
{
    public  class SignupDataAccess: BaseDataAccess,ISignupDataAccess
    {

        public SignupDataAccess(IConfiguration configuration) : base(configuration)
        {

        }

        public string CustomerSignup( CustomerSignup customerSignup)
        {
            string query = $@" Insert into customer(EmailId,Password,Gender,Name,PhoneNumber) select '{customerSignup.Email}','{customerSignup.Password}', '{customerSignup.Gender}','{customerSignup.Name}','{customerSignup.PhoneNumber}'
                            where not exists( 
                                    select * from customer where EmailId = '{customerSignup.Email}' and phoneNumber='{customerSignup.PhoneNumber}')";
            ExecuteNonQuery(query, null, CommandType.Text);

            return "";
        }

        public string RestaurentSignup(RestaurentSignup restaurentSignup)
        {
            string query = $@" Insert into Restaurent(Name,RestaurentName,Address,EmailId,phoneNumber,Gender,PanNumber,GSTIN,FSSAI,
                                  IFSC_Code,AccountNumber,password) 
                                  select '{restaurentSignup.Name}','{restaurentSignup.RestaurentName}',
                                  '{restaurentSignup.Address}','{restaurentSignup.Email}','{restaurentSignup.PhoneNumber}','{restaurentSignup.Gender}',
                                  '{restaurentSignup.PanNumber}','{restaurentSignup.GSTIN}','{restaurentSignup.FSSAI}','{restaurentSignup.IFSC_Code}',
                                  '{restaurentSignup.AccountNumber}','{restaurentSignup.Password}'
                                    where not exists( 
                                        select * from Restaurent where EmailId = '{restaurentSignup.Email}' 
                                         and phoneNumber='{restaurentSignup.PhoneNumber}' and panNumber='{restaurentSignup.PanNumber}')";
            ExecuteNonQuery(query, null, CommandType.Text);

            return "";
        }
        public string DeliveryRiderSignup(DeliveryRiderSignup deliveryRiderSignup)
        {
            string query = $@" Insert into customer(Name,EmailId,phoneNumber,Gender,PanNumber,DrivingLicenseNumber,
                                  IFSC_Code,AccountNumber,password) 
                                  select '{deliveryRiderSignup.Name}','{deliveryRiderSignup.Email}',
                                  '{deliveryRiderSignup.PhoneNumber}','{deliveryRiderSignup.Gender}','{deliveryRiderSignup.PanNumber}',
                                  '{deliveryRiderSignup.DrivingLicenseNumber}','{deliveryRiderSignup.IFSC_Code}','{deliveryRiderSignup.AccountNumber}',
                                   '{deliveryRiderSignup.Password}'
                                    where not exists( 
                                        select * from customer where EmailId = '{deliveryRiderSignup.Email}' 
                                         and phoneNumber='{deliveryRiderSignup.PhoneNumber}' and panNumber='{deliveryRiderSignup.PanNumber}')";
            ExecuteNonQuery(query, null, CommandType.Text);

            return "";
        }
    }
}
