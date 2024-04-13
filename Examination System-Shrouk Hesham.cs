using System;

// Base class for all question types
public abstract class Question
{
    public string Body { get; set; }
    public int Marks { get; set; }
    public string Header { get; set; }

    public abstract void DisplayQuestion();
    public abstract bool CheckAnswer();
}

// True or False question type
public class TrueFalseQuestion : Question
{
    public bool CorrectAnswer { get; set; }

    public override void DisplayQuestion()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Body);
        Console.WriteLine("True or False?");
    }

    public override bool CheckAnswer()
    {
        string answer = Console.ReadLine();
        bool userAnswer = bool.Parse(answer);
        return userAnswer == CorrectAnswer;
    }
}

// Choose One question type
public class ChooseOneQuestion : Question
{
    public string[] Options { get; set; }
    public int CorrectOptionIndex { get; set; }

    public override void DisplayQuestion()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Body);
        for (int i = 0; i < Options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Options[i]}");
        }
    }

    public override bool CheckAnswer()
    {
        string answer = Console.ReadLine();
        int userAnswer = int.Parse(answer) - 1;
        return userAnswer == CorrectOptionIndex;
    }
}

// Choose All question type
public class ChooseAllQuestion : Question
{
    public string[] Options { get; set; }
    public bool[] CorrectOptions { get; set; }

    public override void DisplayQuestion()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Body);
        for (int i = 0; i < Options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Options[i]}");
        }
    }

    public override bool CheckAnswer()
    {
        string[] answers = Console.ReadLine().Split(',');
        bool[] userAnswers = new bool[Options.Length];

        for (int i = 0; i < answers.Length; i++)
        {
            int index = int.Parse(answers[i]) - 1;
            userAnswers[index] = true;
        }

        return CheckArraysEqual(userAnswers, CorrectOptions);
    }

    private bool CheckArraysEqual(bool[] arr1, bool[] arr2)
    {
        if (arr1.Length != arr2.Length)
            return false;

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }

        return true;
    }
}

// Answer class
public class Answer
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}

// Exam base class
public abstract class Exam
{
    public string Subject { get; set; }
    public int Time { get; set; }
    public int NumberOfQuestions { get; set; }
    public Question[] Questions { get; set; }

    public abstract void ShowExam();

    public void GradeExam()
    {
        int totalMarks = 0;
        int obtainedMarks = 0;

        foreach (Question question in Questions)
        {
            question.DisplayQuestion();
            bool isCorrect = question.CheckAnswer();

            totalMarks += question.Marks;
            if (isCorrect)
                obtainedMarks += question.Marks;
        }

        Console.WriteLine();
        Console.WriteLine("Exam Result:");
        Console.WriteLine($"Total Marks: {totalMarks}");
        Console.WriteLine($"Obtained Marks: {obtainedMarks}");
    }
}

// Practice Exam class
public class PracticeExam : Exam
{
    public override void ShowExam()
    {
        Console.WriteLine("Welcome To OOP Exams Done by: Dr.Shrouk Hesham");
        Console.WriteLine($"Subject: {Subject}");
        Console.WriteLine($"Time: {Time} minutes");
        Console.WriteLine($"Number of Questions: {NumberOfQuestions}");
        Console.WriteLine("Practice Exam");

        Console.WriteLine();
        Console.WriteLine("Exam Questions:");
        foreach (Question question in Questions)
        {
            question.DisplayQuestion();
        }

        Console.WriteLine();
        Console.WriteLine("Exam Answers:");
        foreach (Question question in Questions)
        {
            Console.WriteLine(question.CheckAnswer() ? "Correct" : "Incorrect");
        }
    }
}

// Final Exam class
public class FinalExam : Exam
{
    public override void ShowExam()
    {
        Console.WriteLine("Welcome To OOP Exams Done by: Dr.Shrouk Hesham");
        Console.WriteLine($"Subject: {Subject}");
        Console.WriteLine($"Time: {Time} minutes");
        Console.WriteLine($"Number of Questions: {NumberOfQuestions}");
        Console.WriteLine("Final Exam");

        Console.WriteLine();
        Console.WriteLine("Exam Questions:");
        foreach (Question question in Questions)
        {
            question.DisplayQuestion();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create questions for the OOPcourse
        Question q1 = new TrueFalseQuestion()
        {
            Header = "OOP Course",
            Body = "Object-oriented programming (OOP) is a programming paradigm based on the concept of objects.",
            Marks = 1,
            CorrectAnswer = true
        };

        Question q2 = new ChooseOneQuestion()
        {
            Header = "OOP Course",
            Body = "Which of the following is NOT a principle of object-oriented programming?",
            Marks = 1,
            Options = new string[] { "Encapsulation", "Inheritance", "Polymorphism", "Abstraction" },
            CorrectOptionIndex = 3
        };

        Question q3 = new ChooseAllQuestion()
        {
            Header = "OOP Course",
            Body = "Which of the following are access modifiers in C#?",
            Marks = 1,
            Options = new string[] { "public", "private", "protected", "internal" },
            CorrectOptions = new bool[] { true, true, true, true }
        };

        // Create the exam objects
        Exam practiceExam = new PracticeExam()
        {
            Subject = "OOP Course",
            Time = 60,
            NumberOfQuestions = 3,
            Questions = new Question[] { q1, q2, q3 }
        };

        Exam finalExam = new FinalExam()
        {
            Subject = "OOP Course",
            Time = 90,
            NumberOfQuestions = 3,
            Questions = new Question[] { q1, q2, q3 }
        };

        // Select the exam type
        Console.WriteLine("Select the Exam Type:");
        Console.WriteLine("1. Practice Exam");
        Console.WriteLine("2. Final Exam");
        Console.Write("Enter your choice: ");
        int choice = int.Parse(Console.ReadLine());

        // Start the exam
        Console.WriteLine();
        Console.WriteLine("Starting the Exam...");

        Exam selectedExam;
        if (choice == 1)
        {
            selectedExam = practiceExam;
        }
        else
        {
            selectedExam = finalExam;
        }

        selectedExam.ShowExam();

        // Grade the exam
        Console.WriteLine();
        Console.WriteLine("Grading the Exam...");
        selectedExam.GradeExam();

        Console.ReadLine();
    }
}