process.env.NODE_ENV = 'test';
let chai = require('chai');
let chaiHttp = require('chai-http');
let server = require('../index');
let should = chai.should();

chai.use(chaiHttp);

describe('USER TEST REQUESTS', () => {
  describe('/POST Register new user', () => {
    it('it should register new user', (done) => {
      let user = {
        "firstName": "Jonny",
        "lastName": "SilverHand",
        "email": "jonnt123@email.com",
        "password": "jonny"
      }
      chai.request(server)
        .post('/api/user/register')
        .send(user)
        .end((err, res) => {
          res.should.have.status(200);
          res.body.should.be.a('object');
          done();
        });
    });

    it('it should fail registering new user ', (done) => {
      let user = {
        "firstName": undefined,
        "lastName": "",
      }
      chai.request(server)
        .post('/api/user/register')
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
        "email": "jonnt@email.com",
        "password": "jonny"
      }
      chai.request(server)
        .post('/api/user/login')
        .send(user)
        .end((err, res) => {
          res.should.have.status(200);
          res.body.should.be.a('object');
          done();
        });
    });

    it('it should fail login new user ', (done) => {
      let user = {
        "email": "undefined",
        "password": "",
      }
      chai.request(server)
        .post('/api/user/login')
        .send(user)
        .end((err, res) => {
          res.should.have.status(400);
          done();
        });
    });
  });



});