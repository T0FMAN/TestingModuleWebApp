class Question {
	x = getNumb();
	y = getNumb();
	s = getSymb();
	question = getQuest(this.x, this.y, this.s);
	rightAns = calculate(this.x, this.y, this.s);
	userAns;
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

function getQuest(x, y, s) {
	const question = `${x} ${s} ${y}`;
	return question;
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
	new Question(),
	new Question(),
];

const headerContainer = document.querySelector('#header');
const listContainer = document.querySelector('#list');
const submitBtn = document.querySelector('#submit');

let score = 0;
let questionIndex = 0;

clearPage();
showQuestion();
submitBtn.onclick = checkAnswer;

function clearPage() {
	headerContainer.innerHTML = '';
	listContainer.innerHTML = '';
}

function showQuestion() {
	const headerTemplate = '<h2 class="title">%title%</h2>';
	const questNTemplate = '<h2 class="questN">%quest%</h2>';
	const title = headerTemplate.replace('%title%', questions[questionIndex].question);
	const questN = questNTemplate.replace('%quest%', questionIndex+1 + '/' + questions.length)
	headerContainer.innerHTML = questN;
	headerContainer.innerHTML += title;
	headerContainer.innerHTML += '<input id="answer" type="text"/>';
}

function checkAnswer() {
	const userAnswer = parseInt(document.getElementById('answer').value);

	questions[questionIndex].userAns = parseInt(userAnswer);

	if (userAnswer === questions[questionIndex].rightAns) {
		score++;
		questions[questionIndex].isRight = 'Правильно';
	}
	else {
		questions[questionIndex].isRight = 'Ошибка';
    }

	if (questionIndex !== questions.length - 1) {
		questionIndex++;
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

	let title, message;

	title = 'Результаты теста:'

	switch (score) {
		case 2:
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

	headerContainer.innerHTML = sendedMessage;

	console.log(questions);

	var tasksArr = JSON.stringify(questions);

	prepareMail(score, tasksArr);

	submitBtn.blur();
	submitBtn.innerText = 'Начать заново';
	submitBtn.onclick = () => history.go();
}

function prepareMail(score, tasksArr) {
	$.post("/Home/PrepareMail", { score: score, tasksArr: tasksArr})
}