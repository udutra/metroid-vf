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
        float animFrame = 0f;
        float animSpeed = 0f;
        int frameStart;
        int frameEnd;
        int animTotalFrames;
        float timeCounter = 0f;
        public bool doorOpen = false;
        Texture2D texDoorOpen, texDoorClose;

        enum DoorState { Open, Closed, Transition }
        DoorState currentDoorState = DoorState.Closed;

        void EnterDoorState(DoorState newState)
        {
            LeaveDoorState();

            currentDoorState = newState;

            switch (currentDoorState)
            {
                case DoorState.Open:
                    {
                        doorOpen = true;
                        //Animação Porta Aberta
                        doorOp.PlayAnim(2, 2, 1f);
                    }
                    break;

                case DoorState.Transition:
                    {
                        if (doorOpen == true)
                        {
                            //Animação Porta Se fechando
                            doorOp.PlayAnim(0, 2, 2f);
                        }
                        else
                        {
                            //Animação Porta Se Abrindo
                        }
                    }
                    break;

                case DoorState.Closed:
                    {
                        doorOpen = false;
                        //Animação Porta Fechada
                        doorClosed.PlayAnim(0, 2, 10f);
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
                        //TEMPO DA ANIMACAO PORTA SE FECHANDO
                        if (doorOpen == true)
                        {
                            if (timeCounter <= 1f)
                            {
                                timeCounter += dt;
                            }
                            else
                            {
                                EnterDoorState(DoorState.Closed);
                            }
                        }

                        //TEMPO DA ANIMACAO PORTA SE ABRINDO
                        if (doorOpen == false)
                        {
                            if (timeCounter <= 3f)
                            {
                                timeCounter += dt;
                            }
                            else
                            {
                                EnterDoorState(DoorState.Open);
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

        public void PlayAnim(int frameStart, int frameEnd, float animSpeed)
        {
            animFrame = (float)frameStart;
            this.frameStart = frameStart;
            this.frameEnd = frameEnd;
            this.animSpeed = animSpeed;

            animTotalFrames = frameEnd - frameStart + 1;
        }

        public void UpdateAnim(float dt)
        {
            animFrame -= frameStart;

            animFrame += dt * animSpeed;

            animFrame = animFrame % animTotalFrames;
            if (animFrame < 0f)
                animFrame += animTotalFrames;

            animFrame += frameStart;
        }

        public override Vector2 GetSize()
        {
            return new Vector2(32, 80);
        }

        public override Rectangle? GetSourceRectangle()
       {
            if (doorOpen == false)
            {
                return doorOp.GetSourceRectangle((int)doorOp.animFrame);
            }
            return doorClosed.GetSourceRectangle((int)doorClosed.animFrame);
       }

        public override Texture2D GetSprite()
        {
            if(doorOpen==false)
            {
                return doorOp.tex;
            }
            return doorClosed.tex;
        }
       
        public Door(Vector2 initPos) : base(initPos)
        {
           //PRECISA CARREGAR O SHEET DA PORTA
           texDoorOpen = Content.Load<Texture2D>("SpriteSheets/doorSheetOpen");
           texDoorClose = Content.Load<Texture2D>("SpriteSheets/doorSheetClose");
            
           doorOp = new SpriteSheet(texDoorOpen, 3, 1);
           doorClosed = new SpriteSheet(texDoorClose, 3, 1);

            EnterDoorState(DoorState.Closed);
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public override void SetHealth(float f)
        {
            base.SetHealth(f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateDoorState(gameTime);
        }     

    }
}
