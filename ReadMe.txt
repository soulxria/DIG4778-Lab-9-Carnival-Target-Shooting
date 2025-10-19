Object Pooling:
Object pooling is used for the bullets that players shoot. There is and ObjectPooler script that initializes and keeps track of the bullets in the pool. The script contains 
public methods that are called within the PlayerShooting and Bullet scripts to add and remove bullets from the pool.

Builder Pattern:
The builder pattern is used for the creation of the different targets. The TargetBuilder script contains the values of the particular target and has a nested Builder class,
which contains methods that set these values. In the TargetSpawner script, there are methods that spawn each of the different targets with their specified values.

Observer Pattern:
In the GameManager script, there is a static OnScoreUpdate event that is invoked whenever the score is updated. The UI script subscribes to this event and when it is
invoked, it updates the points UI element to show the player's current score.

