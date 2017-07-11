using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class Door : Character
    {
        SpriteSheet doorOp, doorClosed;
        private static ContentManager content;      
        float timeCounter = 0f;
        public bool doorOpen = false;
        Texture2D texDoorOpen, texDoorClose;

        enum DoorState { Open, Closed, Transition }
        DoorState currentDoorState = DoorState.Closed;
        public float health;

        void EnterDoorState(DoorState newState)
        {
            LeaveDoorState();

            currentDoorState = newState;

            switch (currentDoorState)
            {
                case DoorState.Open:
                    {
                        doorOpen = true;
                        timeCounter = 0;
                        //Animação Porta Aberta
                        doorOp.PlayAnim(0, 0, 1f);
                    }
                    break;

                case DoorState.Transition:
                    {
                        doorClosed.PlayAnim(1, 1, 1f);
                    }
                    break;

                case DoorState.Closed:
                    {
                        doorOpen = false;
                        timeCounter = 0;
                        //Animação Porta Fechada
                        doorClosed.PlayAnim(0, 0, 12f);
                    }
                    break;
            }
        }

        void LeaveDoorState()
        {
            switch (currentDoorState)
            {
                case DoorState.Open:
                    {
                        health = 50;
                    }
                    break;

                case DoorState.Transition:
                    {

                    }
                    break;

                case DoorState.Closed:
                    {
                        
                    }
                    break;
            }
        }

        void UpdateDoorState(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentDoorState)
            {
                case DoorState.Open:
                    {
                       //ADICIONAR TRANSIÇÃO DE TELA OU IGNORAR COLISÃO                      
                      //APOS 5 SEGUNDOS A PORTA SE FECHA
                      if (timeCounter <= 5f)
                      {
                          timeCounter += dt;
                      }
                      else
                      {
                          EnterDoorState(DoorState.Transition);
                      }
                    }
                    break;

                case DoorState.Transition:
                    {
                        if (doorOpen == false)
                        {
                            if (timeCounter <= 0.1f)
                            {
                                timeCounter += dt;
                            }
                            else
                            {
                                EnterDoorState(DoorState.Open);
                            }
                        }

                        if (doorOpen == true)
                        {
                            if (timeCounter <= 0.3f)
                            {
                                timeCounter += dt;
                            }
                            else
                            {
                                EnterDoorState(DoorState.Closed);
                            }
                        }

                    }
                    break;

                case DoorState.Closed:
                    {
                        if (health <= 0)
                        {
                            EnterDoorState(DoorState.Transition);
                        }
                    }
                    break;

            }
        }

        public override Vector2 GetSize()
        {
            return new Vector2(64, 96);
        }

        public override Rectangle? GetSourceRectangle()
       {
           if (doorOpen == true)
           {
               return doorOp.GetSourceRectangle((int)doorOp.animFrame);
           }
            return doorClosed.GetSourceRectangle((int)doorClosed.animFrame);
       }

        public override Texture2D GetSprite()
        {
            if(doorOpen==true)
            {
                return doorOp.tex;
            }
            return doorClosed.tex;
        }
       
        public Door(Vector2 initPos) : base(initPos)
        {
           //PRECISA CARREGAR O SHEET DA PORTA
           texDoorOpen = Content.Load<Texture2D>("SpriteSheets/doorSheetClose");
           texDoorClose = Content.Load<Texture2D>("SpriteSheets/doorSheetOpen");
            
           doorOp = new SpriteSheet(texDoorOpen, 3, 1);
           doorClosed = new SpriteSheet(texDoorClose, 3, 1);
            health = 50;
            EnterDoorState(DoorState.Closed);
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void SetHealth(float f)
        {
            health += f;
        }

        public override void Update(GameTime gameTime)
        {
            doorOp.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            doorClosed.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);

            UpdateDoorState(gameTime);
            base.Update(gameTime);
            
        }

        public override bool IgnoreCollision(Entity other)
        {
            if (doorOpen == true)
            {
                if (other is Human)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
