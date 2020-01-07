import { PeopleModule } from './people.module';

describe('PeopleModule', () => {
  let peopleModule: PeopleModule;

  beforeEach(() => {
    peopleModule = new PeopleModule();
  });

  it('should create an instance', () => {
    expect(peopleModule).toBeTruthy();
  });
});
