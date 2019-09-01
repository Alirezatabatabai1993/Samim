using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samim.DataLayer.Helpers
{
	public static class ModelStateExtensionClass
	{
		public static List<KeyValuePair<string, string>> PopulateErrors(this ModelStateDictionary modelState)
		{
			var errors = new List<KeyValuePair<string, string>>();
			foreach(var item in modelState)
			{
				if (item.Value.Errors.Any())
				{
					errors.Add(new KeyValuePair<string, string>(
								item.Key,
								item.Value.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => current + ", " + next)
								));
				}
			}
			return errors;
		}
	}
}
