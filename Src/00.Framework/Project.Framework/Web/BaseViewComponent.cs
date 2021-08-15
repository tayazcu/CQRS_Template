using Microsoft.AspNetCore.Mvc;
using Project.Framework.Commands;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Framework.Web
{
    public class BaseViewComponent : ViewComponent
    {
        protected readonly CommandDispatcher _commandDispatcher;
        protected readonly QueryDispatcher _queryDispatcher;
        protected readonly IResourceManager _resourceManager;

        public BaseViewComponent(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher, IResourceManager resourceManager)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _resourceManager = resourceManager;
        }


    }
}
