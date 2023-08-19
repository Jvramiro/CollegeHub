namespace CollegeHub.Models {
    public class ActivityAnswer {
        public int Answer { get; private set; }
        public int CorrectAnswer { get; private set; }

        public ActivityAnswer(int Answer, int CorrectAnswer) {
            this.Answer = Answer;
            this.CorrectAnswer = CorrectAnswer;
        }
    }
}
