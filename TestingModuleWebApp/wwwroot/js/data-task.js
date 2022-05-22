const arrLabels = document.querySelectorAll('#answer-label');
const submitBtn = document.querySelector('#submit');

submitBtn.onclick = checkAnswer;

async function checkAnswer() {
	const answers = document.querySelectorAll('#user-answer');

	var validModel = true;

	var values = new Array();
	var i = 0;

	answers.forEach(n => {
		var value = n.value.toString();

		if (value !== '') {
			var isValid = validate(value);

			if (!isValid) {
				validModel = false;
				arrLabels[i].innerHTML += 'Неверно';
			}
			values[i] = value;
		}
		else {
			validModel = false;
			arrLabels[i].innerHTML += 'Пустое значение';
		}
		i++;
	});
	i = 0;

	if (!validModel) {
		return;
	}

	var titles = new Array();

	arrLabels.forEach(n => {
		titles[i] = n.innerText;
		i++;
	});

	var arrValues = JSON.stringify(values);
	var arrTitles = JSON.stringify(titles);

	var header = document.querySelector('#header');
	var listContainer = document.querySelector('#list');

	header.innerHTML = '<h2 class="title">Проверка ответов..</h2>';
	listContainer.innerHTML = '<img src="/img/load_circle.gif"/>';

	var load = true;

	while (load) {

		await $.post("/Test/GetResponse", { arrValues: arrValues, arrTitles: arrTitles })
			.done(function (data) {
				var result_str = `Правильно ${data}%`;

				header.innerHTML = `<h2 class="title">${result_str}</h2>`;

				listContainer.innerHTML = '<label class="control-label">Данный результат отправлен преподавателю.<p></p>Желаете пройти тест еще раз?</label><button onclick="history.go()">Начать заново</button>';

				load = false;
			});
	}
}

function validate(value) {
	var arr = value.toString().split('');

	var isValid = arr.every(n =>
		Number.isInteger(parseInt(n))
		|| n.toString() === '.'
		|| n.toString() === ','
		|| n.toString() === '-');

	return isValid;
}
