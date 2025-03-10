﻿ using Microsoft.AspNetCore.Identity;
using CryptoGame.Entities;
using CryptoGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoGame.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenManager tokenManager;


        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager,
            ITokenManager tokenManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenManager = tokenManager;
        }

        public async Task Signup(SignupUserModel signupUserModel)
        {
            var user = new User
            {
                Email = signupUserModel.Email,
                //UserName = signupUserModel.FirstName + signupUserModel.LastName
                UserName = signupUserModel.FirstName + "_" + signupUserModel.LastName
                
            };

            var result = await userManager.CreateAsync(user, signupUserModel.Password);
            if (result.Succeeded)
            {
                //Console.WriteLine("ceva1");
                await userManager.AddToRoleAsync(user, signupUserModel.RoleId);
            }
            

        }

        public async Task<TokenModel> Login(LoginUserModel loginUserModel)
        {
            var user = await userManager.FindByEmailAsync(loginUserModel.Email);
            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, loginUserModel.Password, false);
                if (result.Succeeded)
                {
                    //Create token
                    var token = await tokenManager.CreateToken(user);

                    return new TokenModel { Token = token };
                }
            }

            return null;
        }
    }
}