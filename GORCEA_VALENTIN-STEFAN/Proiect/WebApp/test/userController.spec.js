'use strict';
const chai = require('chai');
const chaiHttp = require('chai-http');
const sinon = require('sinon');
const proxyquire = require('proxyquire');

const expect = chai.expect;

let mySqlMock = require('mysql');

let isNegative = false;
let auth = 1;

chai.use(chaiHttp);

const configService = require('../app/middlewares/configuration.js');


let mockedConnection = {
    query: function (sql, cc, callback) {
        let isTreated = false;

        // ------------------------------------------
        // here start positive test cases input data
        // ------------------------------------------

        if (sql.sql.includes(`SELECT * FROM user where password =`)) {
            if (isNegative && auth == 0) {
                callback(null, undefined, {});
            }
            else {
                callback(null, [{ id: 1, userName: "valentin.gorcea", email: "emailaici", officialName: "mr vali", isTrainer: 1, password: 'dasmq2' }], {});
                auth -= 1;
            }
        }
        if (sql.sql.includes(`SELECT * FROM user where userName`)) {
            if (isNegative) {
                callback(null, undefined, {});
            }
            else {
                if (isNegative) {
                    callback(null, undefined, {});
                }
                else {
                    callback(null, [{ id: 1, userName: "valentin.gorcea", email: "emailaici", officialName: "mr vali", isTrainer: 1, password: 'dasmq2' }], {});
                }
            }
        }
        if (sql.sql.includes(`SELECT * FROM user`)) {
            if (isNegative) {
                callback(null, undefined, {});
            }
            else {
                if (isNegative) {
                    callback(null, undefined, {});
                }
                else {
                    callback(null, [{ id: 1, userName: "valentin.gorcea", email: "emailaici", officialName: "mr vali", isTrainer: 1, password: 'dasmq2' }], {});
                }
            }
        }
        else {
            callback(null, [{ id: 1, userName: "valentin.gorcea", email: "emailaici", officialName: "mr vali", isTrainer: 1, password: 'dasmq2' }], {});
        }

    },
    release: function () { },
    destroy: function () { }
};
let mockedStoragePool = {
    getConnection: function (callback) {
        callback(null, mockedConnection);
    },
    end: function () { },
    on: function () { }
};

let MMDataServer, mySqlConnectionPoolStub;

describe('calling REST APIs', function () {

    //setting up the mocks and stubs
    before(function () {
        this.enableTimeouts(false)
        mySqlConnectionPoolStub = sinon.stub(mySqlMock, 'createPool');
        mySqlConnectionPoolStub.returns(mockedStoragePool);

        let MockedStorageService = proxyquire('../app/models/db.js', {
            'mysql': mySqlMock
        });

        let mockedStorageService = new MockedStorageService(configService);
        //const userRepository = new require('../app/models/repositories/userRepository.js')(configService, MockedStorageService)

        let mockedLogService = require('../app/middlewares/logService.js');

        MMDataServer = require('../app/controllers/mainController.js')(
            mockedLogService,
            configService,
            mockedStorageService
        );

    });

    //********************** */
    //++++++++++start POSITIVE TESTS
    //********************** */

    //++++++++++getting data

    //get loged in user
    describe('Get user me : GET /api/v1.0/ts/user/me', function () {
        context('with valid input', function () {

            it('responds with 200 and returns data regarding loged in user:', function (done) {

                chai.request(MMDataServer)
                    .get('/api/v1.0/ts/user/me')
                    .set('content-type', 'application/json; charset=utf-8')
                    .set('Authorization', JSON.stringify({
                        "userName": "valentin.gorcea",
                        "password": "dmFsaQ=="
                    }))
                    .send()
                    .end(function (err, response) {
                        if (err) {
                            console.log(err);
                        } else {

                            console.log("Returned response: " + response.text);
                            console.log("Returned response: " + response.status);
                            expect(response.status).to.equal(200);
                        }
                        done();
                    });
            });
        });

    });

    describe('Get user me : GET /api/v1.0/ts/user/', function () {
        context('with valid input', function () {

            it('responds with 200 and returns data regarding loged in user:', function (done) {

                chai.request(MMDataServer)
                    .get('/api/v1.0/ts/user/')
                    .set('content-type', 'application/json; charset=utf-8')
                    .set('Authorization', JSON.stringify({
                        "userName": "valentin.gorcea",
                        "password": "dmFsaQ=="
                    }))
                    .send()
                    .end(function (err, response) {
                        if (err) {
                            console.log(err);
                        } else {

                            console.log("Returned response: " + response.text);
                            console.log("Returned response: " + response.status);
                            expect(response.status).to.equal(200);
                        }
                        done();
                    });
            });
        });

    });

    //get loged in user
    describe('Get user me : GET /api/v1.0/ts/user/me', function () {
        context('with valid input', function () {

            it('responds with 500 and returns error for data regarding loged in user:', function (done) {
                isNegative = true;
                auth = 1;
                chai.request(MMDataServer)
                    .get('/api/v1.0/ts/user/me')
                    .set('content-type', 'application/json; charset=utf-8')
                    .set('Authorization', JSON.stringify({
                        "userName": "valentin.gorcea",
                        "password": "dmFsaQ=="
                    }))
                    .send()
                    .end(function (err, response) {
                        if (err) {
                            console.log(err);
                        } else {

                            console.log("Returned response: " + response.text);
                            console.log("Returned response: " + response.status);
                            expect(response.status).to.equal(500);
                        }
                        done();
                    });
            });
        });

    });

    describe('Get user me : GET /api/v1.0/ts/user/', function () {
        context('with valid input', function () {

            it('responds with 500 and returns error for data regarding loged in user:', function (done) {
                isNegative = true;
                auth = 1;
                chai.request(MMDataServer)
                    .get('/api/v1.0/ts/user/')
                    .set('content-type', 'application/json; charset=utf-8')
                    .set('Authorization', JSON.stringify({
                        "userName": "valentin.gorcea",
                        "password": "dmFsaQ=="
                    }))
                    .send()
                    .end(function (err, response) {
                        if (err) {
                            console.log(err);
                        } else {

                            console.log("Returned response: " + response.text);
                            console.log("Returned response: " + response.status);
                            expect(response.status).to.equal(500);
                        }
                        done();
                    });
            });
        });

    });

    //..
    afterEach(function () {
        console.log("_____________________________________________________________________________________________")
    });

    //clearing out stubs
    after(function () {
        mySqlConnectionPoolStub.restore();
    });
});