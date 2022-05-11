class Task {
	x = getNumb();
	y = getNumb();
	s = getSymb();
	task = getTask(this.x, this.y, this.s);
	rightAns = calculate(this.x, this.y, this.s);
	userAns = 0;
	isRight;
}

function getRandomInt(max) {
	var i = Math.floor(Math.random() * max);
	return i;
}

function calculate(x, y, s) {
	if (s === '+') {
		var i = x + y;
		return parseInt(i);
	}
	else {
		var i = x - y;
		return parseInt(i);
	}
}

function getTask(x, y, s) {
	const task = `${x} ${s} ${y}`;
	return task;
}

function getNumb() {
	var x = getRandomInt(15);
	return x;
}

function getSymb() {
	var i = getRandomInt(2);

	if (i === 1) {
		return '+';
	}
	else {
		return '-';
	}
}

const questions = [
	new Task(),
	new Task(),
	new Task(),
	new Task(),
	new Task(),
	new Task(),
];

const headersContainer = document.querySelectorAll('#header');
const listsContainer = document.querySelectorAll('#list');
const submitBtn = document.querySelector('#submit');

let score = 0;
let questionIndex = 0;

clearPage();

showQuestion();
submitBtn.onclick = checkAnswer;

function clearPage() {
	headersContainer.forEach(n => n.innerHTML = '');
	listsContainer.forEach(n => n.innerHTML = '');
}

function showQuestion() {
	headersContainer.forEach(n => {
		const indexTaskTemplate = '<h2 align="left" class="taskIndex">%quest%</h2>';
		const titleTemplate = '<h2 class="taskTitle">Задача: %title%</h2>';

		const indexTask = indexTaskTemplate.replace('%quest%', `${questionIndex + 1}/${questions.length}`);
		const titleTask = titleTemplate.replace('%title%', questions[questionIndex].task);

		n.innerHTML = indexTask;
		n.innerHTML += titleTask;
		n.innerHTML += '<input id="answer" placeholder="0" type="text" required/>';

        questionIndex++;
	});
}

function checkAnswer() {
    const answerSelectors = document.querySelectorAll('#answer');
    
    var index = questionIndex - answerSelectors.length; 

    answerSelectors.forEach(n => {
        questions[index].userAns = parseInt(n.value);
        if (parseInt(n.value) === questions[index].rightAns){
            score++;
		    questions[index].isRight = 'Правильно';
        }
        else {
            questions[index].isRight = 'Ошибка';
        }
        index++;
    });

	if (questionIndex !== questions.length) {
        clearPage();
		showQuestion();
	}
	else {
		clearPage();
		showResults();
	}
}

function showResults() {
	const resultsTemplate = `
	<h2 class="title">%title%</h2>
	<h3 class="summary">%message%</h3>
	<p class="result">%result%</p>`;

	var title, message;

	title = 'Результаты теста:'

	switch (score) {
		case 6:
			message = 'Тест пройден на 100 баллов!';
			break;
		default: message = 'Тест не пройден';
			break;
	}

	let result = `Правильно ${score} из ${questions.length} вопросов`;

	const sendedMessage = resultsTemplate
		.replace('%title%', title)
		.replace('%message%', message)
		.replace('%result%', result);

	headersContainer[1].innerHTML = sendedMessage;

	var tasksArr = JSON.stringify(questions);

	prepareMail(score, tasksArr);

	submitBtn.blur();
	submitBtn.innerText = 'Начать заново';
	submitBtn.onclick = () => history.go();
}

function prepareMail(score, tasksArr) {
	$.post("/Home/PrepareMail", { score: score, tasksArr: tasksArr })
}