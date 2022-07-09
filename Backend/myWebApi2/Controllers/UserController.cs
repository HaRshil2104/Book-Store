﻿using BookStore.Models.Models;
using BookStore.repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace myWebapi2.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new UserRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            try
            {
                var users = _repository.GetUsers(pageIndex, pageSize, keyword);
                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), users);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
        
        [HttpGet]
        [Route("role")]
        /*public IActionResult GetRoles()
        {
            try
            {
                var roles = _repository.GetRoles();
                
                return StatusCode(HttpStatusCode.OK.GetHashCode(),roles);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }*/
        public IActionResult GetRoles()
        {
            try
            {
                var categories = _repository.GetRoles();
                ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
                {
                    Records = categories.Records.Select(c => new RoleModel(c)),
                    TotalRecords = categories.TotalRecords,
                };

                return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex.Message);
            }

        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _repository.GetUser(id);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = _repository.GetUser(model.Id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    user.Firstname = model.Firstname;
                    user.Lastname = model.Lastname;
                    user.Email = model.Email;

                    var isSaved = _repository.UpdateUser(user);
                    if (isSaved)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail updated successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var user = _repository.GetUser(id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    var isDeleted = _repository.DeleteUser(user);
                    if (isDeleted)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}