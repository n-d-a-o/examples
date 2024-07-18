document.addEventListener('DOMContentLoaded', function ()
{
	const key = `scrollbox.${location.pathname}`;
	const value = sessionStorage.getItem(key);
	if (value)
	{
		const to = parseInt(value, 10);
		window.scrollTo(0, to);
	}

	const buttons = document.querySelectorAll('button');
	buttons.forEach(function (button)
	{
		button.addEventListener('click', function ()
		{
			const key = `scrollbox.${location.pathname}`;
			const value = window.scrollY;
			sessionStorage.setItem(key, value);
		});
	});
});
