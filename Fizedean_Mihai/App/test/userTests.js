let chai = require('chai');
let chaiHttp = require('chai-http');
let server = require('../app');
chai.should();

chai.use(chaiHttp);

describe('USER TEST REQUESTS', () => {
  describe('/POST Register new user', () => {
    it('it should register new user', (done) => {
      let user = {
        "name": "test_8",
        "email": "test_8@gmail.com",
        "password": "test1",
        "password2": "test1"
      }
      chai.request(server)
        .post('/users/register')
        .send(user)
        .end((err, res) => {
          res.should.redirectTo('http://localhost:5000/users/login');
          done();
        });
    });

    it('it should fail registering new user ', (done) => {
      let user = {
        "name": "123",
        "email": "123@email.com",
        "password": "123456",
        "password": "1234567"
      }
      chai.request(server)
        .post('/users/register')
        .send(user)
        .end((err, res) => {
          res.should.have.status(400);
          done();
        });
    });

    
  });

  describe('/POST Login created user', () => {
    it('log in new user', (done) => {
      let user = {
        "email": "mihai.fizedean@gmail.com",
        "password": "themitz97"
      }
      chai.request(server)
        .post('/users/login')
        .send(user)
        .end((err, res) => {
          res.should.redirectTo('http://localhost:5000/');
          done();
        });
    });

    it('it should fail login new user ', (done) => {
      let user = {
        "email": "test_1@gmail.com",
        "password": "blablabla",
      }
      chai.request(server)
        .post('/users/login')
        .send(user)
        .end((err, res) => {
          res.should.redirectTo('http://localhost:5000/users/login');
          done();
        });
    });
  });

  describe('/GET Logout user', () => {
    it('logout user', (done) => {
      chai.request(server)
        .get('/users/logout')
        .end((err, res) => {
          res.should.redirectTo('http://localhost:5000/users/login');
          done();
        });
    });
  });



});