using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models.Components.Breadcrumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Components
{
    public class BreadcrumbsWidget : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<BreadcrumpsItem> items)
        {
            if (items == null || !items.Any())
                throw new ArgumentException($"Breadcrumb can't be create. Incorrect parameters: {items}.");

            var viewModel = items?.Select(ConvertToBreadcrumpsViewModel).ToList();
            viewModel[viewModel.Count - 1].IsActive = true;

            return View(viewModel);
        }

        private BreadcrumpsViewModel ConvertToBreadcrumpsViewModel(BreadcrumpsItem input)
        {
            if (input == null)
                return null;

            return new BreadcrumpsViewModel
            {
                UIName = $"({input.Action}+{input.Controller})",
                Controller = input.Controller,
                Action = input.Action,
                IsActive = false
            };
        }
    }
}
