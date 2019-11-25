﻿using System;
using SadConsole.Entities;
using SadRogue.Primitives;

namespace SadConsole.Components
{
    /// <summary>
    /// Add to an <see cref="Entity"/> to sync the visibility and position offset with a parent <see cref="Console"/>. 
    /// </summary>
    public class EntityViewSync : UpdateComponent
    {
        private Point _oldPosition;
        private Rectangle _oldView;

        /// <summary>
        /// If set to true, controls the <see cref="IScreenObject.IsVisible"/> property of the attached object.
        /// </summary>
        public bool HandleIsVisible { get; set; } = true;

        /// <inheritdoc/>
        public override void OnAdded(IScreenObject host)
        {
            if (!(host is Entity))
                throw new Exception($"{nameof(EntityViewSync)} can only be added to an {nameof(Entity)}.");
        }


        /// <inheritdoc />
        public override void Update(IScreenObject hostObject)
        {
            var host = (Entity)hostObject;

            if (host.Parent is IScreenObjectSurface parent)
            {
                Rectangle parentViewPort = parent.Surface.GetViewRectangle();

                if (parentViewPort != _oldView || host.Position != _oldPosition)
                {
                    host.PositionOffset = new Point(-parentViewPort.Position.X, -parentViewPort.Position.Y).TranslateFont(parent.FontSize, host.FontSize);

                    if (HandleIsVisible)
                        host.IsVisible = parent.AbsoluteArea.Contains(host.AbsolutePosition);

                    _oldPosition = host.Position;
                    _oldView = parentViewPort;
                }
            }
        }

        /// <inheritdoc />
        public override void OnRemoved(IScreenObject host)
        {
            _oldPosition = (0, 0);
            _oldView = Rectangle.Empty;

            host.IsVisible = true;
            ((Entity)host).PositionOffset = (0, 0);
        }
    }
}