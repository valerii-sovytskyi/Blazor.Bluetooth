using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SampleClientSide.Helpers
{
	/// <summary>
	/// Bindable base.
	/// </summary>
	public abstract class BindableBase : ComponentBase
	{
		/// <summary>
		/// Set property with StateHasChanged call.
		/// </summary>
		/// <typeparam name="T">Any type of property.</typeparam>
		/// <param name="property">Property ref.</param>
		/// <param name="value">Value.</param>
		/// <param name="propertyName">Property name, by default CallerMemberName</param>
		/// <returns>Indicate if property changed.</returns>
		protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
		{
			if (!EqualityComparer<T>.Default.Equals(property, value))
			{
				property = value;
				StateHasChanged();
				return true;
			}

			return false;
		}
	}
}
