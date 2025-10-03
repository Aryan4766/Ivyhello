namespace Aryan.Apps;

using Stateless;

[App(icon: Icons.Workflow, title: "Stateless")]
public class StatelessApp : ViewBase
{
	private enum TrafficLightState { Red, Green, Yellow }
	private enum Trigger { Timer, Emergency }

	public override object? Build()
	{
		var machineState = this.UseState(TrafficLightState.Red);

		// Bind Stateless state to Ivy state using accessor/mutator
		var stateMachine = new StateMachine<TrafficLightState, Trigger>(
			() => machineState.Value,
			s => machineState.Value = s
		);

		stateMachine.Configure(TrafficLightState.Red)
			.Permit(Trigger.Timer, TrafficLightState.Green)
			.PermitReentry(Trigger.Emergency);

		stateMachine.Configure(TrafficLightState.Green)
			.Permit(Trigger.Timer, TrafficLightState.Yellow)
			.Permit(Trigger.Emergency, TrafficLightState.Red);

		stateMachine.Configure(TrafficLightState.Yellow)
			.Permit(Trigger.Timer, TrafficLightState.Red)
			.Permit(Trigger.Emergency, TrafficLightState.Red);

		object Header() => Layout.Vertical().Gap(2)
			| Text.H2("Traffic Light (Stateless)")
			| Text.Block($"Current state: {machineState.Value}");

		object Controls()
		{
			var canTimer = stateMachine.CanFire(Trigger.Timer);
			var canEmergency = stateMachine.CanFire(Trigger.Emergency);

			return Layout.Horizontal().Gap(3)
				| new Button("Timer", onClick: () =>
				{
					if (stateMachine.CanFire(Trigger.Timer))
					{
						stateMachine.Fire(Trigger.Timer);
					}
				}).Disabled(!canTimer)
				| new Button("Emergency", onClick: () =>
				{
					if (stateMachine.CanFire(Trigger.Emergency))
					{
						stateMachine.Fire(Trigger.Emergency);
					}
				}).Disabled(!canEmergency)
				| new Button("Reset to Red", onClick: () =>
				{
					// Force back to Red using the reentry trigger when possible, otherwise set state
					if (stateMachine.CanFire(Trigger.Emergency))
					{
						stateMachine.Fire(Trigger.Emergency);
					}
					else
					{
						machineState.Value = TrafficLightState.Red;
					}
				});
		}

		object Diagram() => Layout.Vertical().Gap(1)
			| Text.Block(machineState.Value == TrafficLightState.Red ? "● Red" : "○ Red")
			| Text.Block(machineState.Value == TrafficLightState.Yellow ? "● Yellow" : "○ Yellow")
			| Text.Block(machineState.Value == TrafficLightState.Green ? "● Green" : "○ Green");

		return Layout.Center()
			| new Card(
				Layout.Vertical().Gap(6)
				| Header()
				| Text.Block("This demo uses the Stateless state machine to model a simple traffic light.")
				| Controls()
				| new Separator()
				| Diagram()
			)
			.Width(Size.Units(120).Max(600));
	}
}


