import { CustomDatetimePipe } from './custom-datetime.pipe';

describe('CustomDatetimePipe', () => {
  it('create an instance', () => {
    const pipe = new CustomDatetimePipe();
    expect(pipe).toBeTruthy();
  });
});
