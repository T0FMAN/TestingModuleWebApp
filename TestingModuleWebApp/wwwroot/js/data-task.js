class PhyTask{
	TaskText;
	m;
	R;
	zE;
	t;
	x0;
	u0;
	a0;
	_x0;
	_u0;
	_a0;
	_p0;
	_Ek0;
	_u;
	_Ek;
	_F;
	_S;
	_A;
	_N;
}

//const arrLabels = document.querySelectorAll('#answer-label');
const submitBtn = document.querySelector('#submit');

submitBtn.onclick = checkAnswer;

const header = document.querySelector('#header');
const listContainer = document.querySelector('#list');

let arrValues;
let arrTitles;

let task = new PhyTask();

let isValid = true;

function setTask(data) {
	var _task = JSON.parse(data);

	task.TaskText = _task.TaskText;
	task.m = _task.m;
	task.R = _task.R;
	task.zE = _task.zE;
	task.t = _task.t;
	task.x0 = _task.x0;
	task.u0 = _task.u0;
	task.a0 = _task.a0;
}

async function getTitles() {
	var titles = new Array();

	await $.post("/Test/GetGroupsTitle")
		.done(function (data) {
			var count = data.length;

			for (var i = 0; i < count; i++) {
				titles[i] = data[i];
			}
		});

	return titles;
}

async function inputName() {
	var titles = await getTitles();
	var tempOpt = '';

	var taskText = document.querySelector('#title-task');
	taskText.innerHTML = '';
	var taskImg = document.getElementById('image-task');
	taskImg.parentNode.removeChild(taskImg);

	titles.forEach(n => tempOpt += `<option value="${n}">${n}</option>`);

	var templateTitles =
		`<div class="col">
			<select id="groupsTitles" class="form-control form-control-lg">
				%titles%
			</select>
			<span class="text-danger"></span>
		</div>`;

	var newStr = templateTitles.replace('%titles%', tempOpt);

	header.innerHTML = '<h2 class="title">Ответы записаны</h2>';
	listContainer.innerHTML = `<p>Введите свои данные для сохранения результатов</p>
							   <p></p>
								<label>Фамилия:</label>
							   <input id="lastname" placeholder="Иванов"/>
							   <p></p>
								<label>Имя:</label>
							   <input id="firstname" placeholder="Иван"/>
							   <p></p>
								<label>Группа:</label>
							    ${newStr}
							   <div>
									<button id="send" onclick="sendData()">Ответить</button>
							   </div>`;
}

async function sendData() {
	const selectedGroup = document.querySelector('#groupsTitles').value;
	const selectedName = document.querySelector('#firstname').value;
	const selectedFam = document.querySelector('#lastname').value;

	if (selectedName.replace(/\s/g, '') === "" || selectedName.length < 2 || selectedFam.replace(/\s/g, '') === "" || selectedFam.length < 2) {
		alert('Имя или фамилия не могут быть пустыми, или содержать меньше 2-ух символов');
		return;
    }

	var load = true;

	var phyTask = JSON.stringify(task);

	while (load) {
		await $.post("/Test/GetResponse", { phyTask: phyTask, selectedGroup: selectedGroup, selectedName: selectedName, selectedFam: selectedFam })
			.done(function (data) {
				var result_str = `Правильно ${data}%`;

				header.innerHTML = `<h2 class="title">${result_str}</h2>`;

				listContainer.innerHTML = '<label class="control-label">Данный результат отправлен преподавателю.<p></p>Желаете пройти тест еще раз?</label><button onclick="history.go()">Начать заново</button>';

				load = false;
			});
	}
}

async function checkAnswer() {
	var x0 = validate(document.querySelector('#x0'), "Начальная координата (X₀)");
	var u0 = validate(document.querySelector('#u0'), "Начальная скорость (U₀)");
	var a0 = validate(document.querySelector('#a0'), "Касательное ускорение. Ускорение тела (a)");
	var p0 = validate(document.querySelector('#p0'), "Начальный импульс тела (p₀)");
	var Ek0 = validate(document.querySelector('#Ek0'), "Начальная кинетическая энергия (Eₖ₀)");
	var u = validate(document.querySelector('#u'), `Скорость тела через ${task.t}c. (U)`);
	var Ek = validate(document.querySelector('#Ek'), `Кинетическая энергия ${task.t}c. (Eₖ)`);
	var F = validate(document.querySelector('#F'), "Равнодействующая сила (F)");
	var S = validate(document.querySelector('#S'), `Перемещение за ${task.t}c. (S)`);
	var A = validate(document.querySelector('#A'), `Работа равнодействующей силы в течении ${task.t}c. (A)`);
	var N = validate(document.querySelector('#N'), "Мощность механическая (N)");

	if (!isValid) {
		isValid = true;
		return;
    }

	task._x0 = x0;
	task._u0 = u0;
	task._a0 = a0;
	task._p0 = p0;
	task._Ek0 = Ek0;
	task._u = u;
	task._Ek = Ek;
	task._F = F;
	task._S = S;
	task._A = A;
	task._N = N;

	inputName();
}

function validate(node, text) {
	var value = node.value;

	if (!isValid)
		return value;

	var parse = parseFloat(value);

	if (isNaN(parse)) {
		alert(`Значение '${text}' имеет неверный формат: ${value}\r\nПримеры ответов:\r\n1) 3,14\r\n2) 56`);
		isValid = false;
    }

	console.log(`${text} - ${value}`);

	return value;
}
