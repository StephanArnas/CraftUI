using System.Collections;
using System.Collections.Specialized;
using CraftUI.Library.Maui.Controls;

namespace CraftUI.Library.Maui.Common;

internal class SelectionList : IList<object>
{
	private static readonly IList<object> SEmpty = new List<object>(0);
	private readonly CfMultiPickerPopup _selectableItemsView;
	private readonly IList<object> _internal;
	private IList<object> _shadow;
	private bool _externalChange;

	public SelectionList(CfMultiPickerPopup selectableItemsView, IList<object>? items = null)
	{
		_selectableItemsView = selectableItemsView ?? throw new ArgumentNullException(nameof(selectableItemsView));
		_internal = items ?? new List<object>();
		_shadow = Copy();

		if (items is INotifyCollectionChanged incc)
		{
			incc.CollectionChanged += OnCollectionChanged;
		}
	}

	public object this[int index] { get => _internal[index]; set => _internal[index] = value; }

	public int Count => _internal.Count;

	public bool IsReadOnly => false;

	public void Add(object item)
	{
		_externalChange = true;
		_internal.Add(item);
		_externalChange = false;

		_selectableItemsView.SelectedItemsPropertyChanged(_shadow, _internal);
		_shadow.Add(item);
	}

	public void Clear()
	{
		_externalChange = true;
		_internal.Clear();
		_externalChange = false;

		_selectableItemsView.SelectedItemsPropertyChanged(_shadow, SEmpty);
		_shadow.Clear();
	}

	public bool Contains(object item)
	{
		return _internal.Contains(item);
	}

	public void CopyTo(object[] array, int arrayIndex)
	{
		_internal.CopyTo(array, arrayIndex);
	}

	public IEnumerator<object> GetEnumerator()
	{
		return _internal.GetEnumerator();
	}

	public int IndexOf(object item)
	{
		return _internal.IndexOf(item);
	}

	public void Insert(int index, object item)
	{
		_externalChange = true;
		_internal.Insert(index, item);
		_externalChange = false;

		_selectableItemsView.SelectedItemsPropertyChanged(_shadow, _internal);
		_shadow.Insert(index, item);
	}

	public bool Remove(object item)
	{
		_externalChange = true;
		var removed = _internal.Remove(item);
		_externalChange = false;

		if (removed)
		{
			_selectableItemsView.SelectedItemsPropertyChanged(_shadow, _internal);
			_shadow.Remove(item);
		}

		return removed;
	}

	public void RemoveAt(int index)
	{
		_externalChange = true;
		_internal.RemoveAt(index);
		_externalChange = false;

		_selectableItemsView.SelectedItemsPropertyChanged(_shadow, _internal);
		_shadow.RemoveAt(index);
	}

	List<object> Copy()
	{
		var items = new List<object>();
		for (int n = 0; n < _internal.Count; n++)
		{
			items.Add(_internal[n]);
		}

		return items;
	}

	private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
	{
		if (_externalChange)
		{
			// If this change was initiated by a renderer or direct manipulation of CollectionView.SelectedItems,
			// we don't need to send a selection change notification
			return;
		}

		// This change is coming from a bound viewmodel property
		// Emit a selection change notification, then bring the shadow copy up-to-date
		_selectableItemsView.SelectedItemsPropertyChanged(_shadow, _internal);
		_shadow = Copy();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}