<img src="docs/images/unity-wide.png" align="middle" width="3000"/>

<img src="docs/images/image-banner.png" align="middle" width="3000"/>

# Introduction 

This first delivery of the input/output course at ENTI delivered during Spring 2019 is focused on showing you understood the basic notions of modern machine learning, and how to use such techniques as they are available in Unity. Your goal is to analyze  one of the basic examples, dissect it, understand it, and apply the same strategy to a different problem.

## Assets 

The assets to explore and use are the ones available at the ml-agents repository, which for this course I have forked [here]( https://github.com/joanllobera/ml-agents.git). It is also possible to add examples from the Marathon environmetns, which can be found [here](https://github.com/Unity-Technologies/marathon-envs)
 
 
 ![MarathonEnvs](images/MarathonEnvsBanner.gif)

## Delivery format 

In delivery you must deliver a notebook reflecting the exercises outlined below. You will also be asked to go through your notebook in class, explaining to the class what you did, and how to reproduce your results.

A complementary scene or example implemented may also be included in the delivery.



## Exercises 

The notebook will have the following sections:
1. Introduction
A short paragraph explaining what is the aim of this notebook, and a specification of the team involved (at least: picture, name, surname, email)

2. Case analysis 
You explain clearly how an example that is already implemented works. This includes at least an explanation of:
 a) what are the rewards, 
 b) what are the states, 
 c) how is, in general, the training implemented 

3. Performance analysis 
You do a performance analysis of the training using TensorBoard, exploring what parameters are most critical to a) make it work, b) produce smooth results, and c) train fast. In your analysis you should include a combination of parameters that does not work, and some that do work. You should also show some understanding of the role of the parameters, and try to explain why something works or doesn't.

4. New case proposal 
Taking as basis the example analysed, you propose a different task for the same training procedure, or a very similar one. You explain how to train the new use case, nad how the setup, rewards, states or training change, compared to the example. 

A bonus would be to provide a (rough) implementation of this new use case. 


## Grades

The grades will follow these guidelines:
1. Quality of documentation and class presentation (+1)
2. Case analysis (+3)
3. Performance analysis (+2)
4. New case proposal (+4)

## Extra requirements

* The notebook and code that you propose should work straight away from downloading the repository. 
* Your username should reflect your name+surname
* Your delivery should have a tag labelled `ENTI_IO2019_D1_GXX`, where *XX* stand for your group number
* Your repository contributions should follow the git flow conventions. You should work on one or several  separate branches, and perform a merge request once satisfied with your contribution. The branch to which you will want to merge is called `develop-ENTI-delivery1`
* The files should be organized preserving the structure already set up in the repository
* Your contributions should not collide with the ones from other development teams

All the previous requirements should be satisfied for the submission to be evaluated. 


