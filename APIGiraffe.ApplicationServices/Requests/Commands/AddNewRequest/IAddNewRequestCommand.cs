﻿namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest
{
    public interface IAddNewRequestCommand
    {
        void Execute(int groupId, string name);
    }
}