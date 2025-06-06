let users = { name: "Davi Rodrigues", email: "davi@gmail.com", password: "senha123" };

document.addEventListener("DOMContentLoaded", function () {
  if (localStorage.getItem("login")) {
    window.location = "../dashboard/dashboard.html"
  }

  createTestingDB();

  const loginButton = document.getElementById("login");
  const emailInput = document.getElementById("email");
  const passwordInput = document.getElementById("password");

  loginButton.addEventListener("click", function () {
    let login = {email: emailInput.value, password: passwordInput.value }
    if (JSON.stringify([login.email, login.password]) == JSON.stringify([users.email, users.password])) {
      localStorage.setItem("login", JSON.stringify(users))
      window.location = "../dashboard/dashboard.html";
    }
    else {
      alert("Login errado!");
    }
  })
})


function createTestingDB() {
  if (localStorage.length == 0) {

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Mario",
      email: "mario@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-05-04T09:27:58",
      age: 29,
      adress: "Rua ABC, 123",
      other: "Gosta de livros",
      interests: "Tecnologia",
      feelings: "Motivado",
      values: "Honestidade"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "João",
      email: "joao@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-05-04T09:27:53",
      age: 29,
      adress: "Rua das Flores, 123",
      other: "Gosta de café",
      interests: "Tecnologia",
      feelings: "Motivado",
      values: "Honestidade"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Maria",
      email: "maria@gmail.com",
      status: "Inativo",
      pending: false,
      date: "2025-01-06T14:03:12",
      age: 34,
      adress: "Av. Paulista, 987",
      other: "Tem um gato",
      interests: "Literatura",
      feelings: "Tranquila",
      values: "Respeito"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Carlos",
      email: "carlos@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-02-08T11:23:45",
      age: 41,
      adress: "Rua A, 45",
      other: "Corre maratonas",
      interests: "Esportes",
      feelings: "Energético",
      values: "Determinação"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Rafael",
      email: "rafael@gmail.com",
      status: "Inativo",
      pending: false,
      date: "2025-03-19T04:57:12",
      age: 28,
      adress: "Rua do Comércio, 88",
      other: "Viaja muito",
      interests: "Fotografia",
      feelings: "Reflexivo",
      values: "Liberdade"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Fernanda",
      email: "fernanda@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-06-01T08:15:30",
      age: 32,
      adress: "Travessa Central, 11",
      other: "Vegetariana",
      interests: "Culinária",
      feelings: "Feliz",
      values: "Saúde"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Bruno",
      email: "bruno@gmail.com",
      status: "Inativo",
      pending: false,
      date: "2025-04-12T19:42:10",
      age: 36,
      adress: "Rua Nova, 99",
      other: "Toca violão",
      interests: "Música",
      feelings: "Calmo",
      values: "Criatividade"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Luciana",
      email: "luciana@gmail.com",
      status: "Ativo",
      pending: false,
      date: "2025-05-22T13:09:55",
      age: 40,
      adress: "Alameda Verde, 27",
      other: "Fala 3 idiomas",
      interests: "Idiomas",
      feelings: "Confiante",
      values: "Conhecimento"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Eduardo",
      email: "eduardo@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-03-03T07:21:18",
      age: 25,
      adress: "Rua Azul, 300",
      other: "Ama filmes antigos",
      interests: "Cinema",
      feelings: "Curioso",
      values: "Tradição"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Paula",
      email: "paula@gmail.com",
      status: "Inativo",
      pending: true,
      date: "2025-02-27T16:33:47",
      age: 37,
      adress: "Rua das Palmeiras, 50",
      other: "Tem um blog",
      interests: "Escrita",
      feelings: "Criativa",
      values: "Expressão"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "André",
      email: "andre@gmail.com",
      status: "Ativo",
      pending: false,
      date: "2025-04-05T10:12:40",
      age: 30,
      adress: "Rua Oeste, 15",
      other: "Surfista",
      interests: "Natureza",
      feelings: "Livre",
      values: "Aventura"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Camila",
      email: "camila@gmail.com",
      status: "Inativo",
      pending: true,
      date: "2025-01-18T22:44:07",
      age: 31,
      adress: "Av. Central, 221",
      other: "Trabalha com moda",
      interests: "Design",
      feelings: "Inspirada",
      values: "Estilo"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Thiago",
      email: "thiago@gmail.com",
      status: "Ativo",
      pending: false,
      date: "2025-03-09T09:57:33",
      age: 38,
      adress: "Rua do Sol, 10",
      other: "Pai de dois",
      interests: "Família",
      feelings: "Realizado",
      values: "Compromisso"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Juliana",
      email: "juliana@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-05-11T17:03:26",
      age: 27,
      adress: "Rua Leste, 3",
      other: "Faz voluntariado",
      interests: "Ação social",
      feelings: "Solidária",
      values: "Empatia"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Fábio",
      email: "fabio@gmail.com",
      status: "Inativo",
      pending: false,
      date: "2025-06-02T06:28:55",
      age: 44,
      adress: "Av. Sul, 77",
      other: "Gosta de jardinagem",
      interests: "Natureza",
      feelings: "Paciente",
      values: "Simplicidade"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Tatiane",
      email: "tatiane@gmail.com",
      status: "Ativo",
      pending: false,
      date: "2025-02-15T15:14:11",
      age: 35,
      adress: "Rua Bela Vista, 56",
      other: "Tem uma startup",
      interests: "Inovação",
      feelings: "Ambiciosa",
      values: "Progresso"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Marcelo",
      email: "marcelo@gmail.com",
      status: "Inativo",
      pending: true,
      date: "2025-04-29T21:19:38",
      age: 42,
      adress: "Rua Velha, 81",
      other: "Faz podcasts",
      interests: "Comunicação",
      feelings: "Expressivo",
      values: "Transparência"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Vanessa",
      email: "vanessa@gmail.com",
      status: "Ativo",
      pending: true,
      date: "2025-03-25T12:07:59",
      age: 33,
      adress: "Av. do Lago, 66",
      other: "Faz yoga",
      interests: "Bem-estar",
      feelings: "Equilibrada",
      values: "Harmonia"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Igor",
      email: "igor@gmail.com",
      status: "Inativo",
      pending: false,
      date: "2025-01-30T03:51:02",
      age: 39,
      adress: "Rua Industrial, 101",
      other: "Coleciona vinis",
      interests: "Música",
      feelings: "Nostálgico",
      values: "Autenticidade"
    }));

    localStorage.setItem(crypto.randomUUID(), JSON.stringify({
      name: "Renata",
      email: "renata@gmail.com",
      status: "Ativo",
      pending: false,
      date: "2025-05-17T14:39:00",
      age: 26,
      adress: "Rua do Parque, 5",
      other: "Corre nas manhãs",
      interests: "Saúde",
      feelings: "Animada",
      values: "Disciplina"
    }));

  }
}